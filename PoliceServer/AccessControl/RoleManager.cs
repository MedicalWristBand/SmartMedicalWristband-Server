using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceServer.Annotation;
using PoliceServer.Enums;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;

namespace PoliceServer.AccessControl
{
    public class RoleManager
    {
        #region ===== Roles Definition =====

        private static Role _systewAdminRole;
        private static Role _doctoRole;
        private static Role _nurseRole;
        private static Role _staffRole;
        private static Role _noneRole;

        #endregion ===== Roles Definition =====

        private readonly Dictionary<FarsiPage, List<Role>> _headerPermissions;
        private readonly Dictionary<string, List<Role>> _pagePermissions;
        private List<Role> _allRoles;
        private static Dictionary<RoleType, string> _roleFarsi;
        private readonly List<User> _onlineUsers;
        private static RoleManager _instance;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly PoliceContext _context;

        private RoleManager()
        {
            _context = Utilities.ContextCreator.GetInstance().GetContext();

            _CreateRoles();
            _headerPermissions = new Dictionary<FarsiPage, List<Role>>();
            _pagePermissions = new Dictionary<string, List<Role>>();
            _roleFarsi = new Dictionary<RoleType, string>();

            _FillHeaderPermissions();
            _FillPagePermissions();
            _FillRoleFarsi();

            _onlineUsers = new List<User>();
            Log.Debug("A Role Manager Has Been Created");
        }

        public static RoleManager GetInstance()
        {
            return _instance ?? (_instance = new RoleManager());
        }

        /// <summary>
        /// این تابع در ابتدای هر اجرای برنامه صدا می‌شود و تمام رول ها را میسازد.
        /// </summary>
        private void _CreateRoles()
        {
            _doctoRole = new Role(RoleType.Doctor);
            _noneRole = new Role(RoleType.None);
            _nurseRole = new Role(RoleType.Nurse);
            _staffRole = new Role(RoleType.Staff);
            _systewAdminRole = new Role(RoleType.SystemAdmin);

            _systewAdminRole.CanAssignRole(_systewAdminRole);
            _systewAdminRole.CanAssignRole(_doctoRole);
            _systewAdminRole.CanAssignRole(_nurseRole);
            _systewAdminRole.CanAssignRole(_staffRole);
            _systewAdminRole.CanAssignRole(_noneRole);

            _allRoles = new List<Role>()
            { 
                _systewAdminRole,
                _doctoRole,
                _nurseRole,
                _staffRole,
                _noneRole
            };
        }

        /// <summary>
        /// این تابع سر منو ها را ایجاد میکند
        /// </summary>
        private void _FillHeaderPermissions()
        {
            _headerPermissions.Add(new FarsiPage() {FarsiName = "خانه", Parent = HeaderType.Home, Icon = "clip-home"},
                _allRoles);
            _headerPermissions.Add(
                new FarsiPage() {FarsiName = "گزارش ها", Parent = HeaderType.Report, Icon = "clip-stats"},
                new List<Role>() {_systewAdminRole});
            _headerPermissions.Add(
                new FarsiPage() {FarsiName = "منوی کاربری", Parent = HeaderType.Setting, Icon = "clip-user-5"},
                _allRoles);
            _headerPermissions.Add(
                new FarsiPage() { FarsiName = "تولید بارکد", Parent = HeaderType.generateBarcode, Icon = "clip-stats" },
                new List<Role>() { _systewAdminRole });

        }

        /// <summary>
        /// این تابع برای ایجاد دسترسی های صفحات است
        /// </summary>
        private void _FillPagePermissions()
        {
            _pagePermissions.Add("/default/index", _allRoles);
            _pagePermissions.Add("/account/managepassword", _allRoles);
            _pagePermissions.Add("/account/manageaccess", new List<Role>(){_systewAdminRole});
            _pagePermissions.Add("/register/registeruser", new List<Role>() { _systewAdminRole });
            _pagePermissions.Add("/generatebarcode/generatebarcode", new List<Role>() { _systewAdminRole });

            Log.Debug("Permission Of RoleManager Has Been Added To List");
        }

        /// <summary>
        /// یک دیکشنری از رول ها و اسم فارسی آنها برمیگرداند
        /// </summary>
        private void _FillRoleFarsi()
        {
            foreach (Role role in _allRoles)
            {
                _roleFarsi.Add(role.GetRoleType(), role.GetFarsiRole());
            }
        }

        /// <summary>
        /// توضیح فارسی مربوط به وظیفه را باز می گرداند
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public String GetFarsiRole(RoleType role)
        {
            // TODO: Line Below Should Never Existed
            if (_roleFarsi.ContainsKey(role))
            {
                return _roleFarsi[role];
            }
            return null;
        }

        /// <summary>
        /// بررسی می کند که آیا کاربر اجازه ورود دارد یا خیر 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>اگر کاربر اجازه ی ورود داشته باشد مقدار درست بر می گرداند</returns>
        /// <remarks> 
        /// ممکن است کاربری اجازه ی ورود داشته باشد اما به هیچ بخشی دسترسی نداشته باشد
        /// </remarks>
        public bool CheckPassword(String username, String password)
        {
            try
            {
                Log.DebugFormat(LogMessage.CheckPasswordBegin, username);
                User user = Repository.UserRepository.GetInstance().FindByNationalCode(username);
//                if (user.Status == Status.Deleted)
//                {
//                    // banned user try to login!
//                    return false;
//                }
//                else if (user.IsService) // کاربرانی که تنها مجاز به فراخوانی سرویس ها هستند
//                {
//                    return false;
//                }
                if (user.Password.Equals(password)) // password entered by user is true
                {
                    Log.DebugFormat(LogMessage.CheckPasswordTrue, username);
                    Log.DebugFormat(LogMessage.CheckPasswordFinished, username);
                    return true;
                } // password entered by user is not true
                else
                {
                    Log.DebugFormat(LogMessage.CheckPasswordFalse, username);
                    Log.DebugFormat(LogMessage.CheckPasswordFinished, username);
                    return false;
                }
            }
            catch (EntityNotFoundException ex)
            {
                Log.Warn(LogMessage.CheckPasswordWarning, ex);
                return false;
            }
            catch (UserInterfaceException ex) // Perhaps Format is not true or entity not found
            {
                if (ex.ErrorCode == 74401)
                {
                    return false;
                }
                throw;
            }
            catch (SqlException ex)
            {
                Log.Error(LogMessage.CheckPasswordSqlException, ex);
                throw new UserInterfaceException(
                    "خطای 101:امکان اتصال سیستم با مرکز اطلاعات فراهم نمی باشد. با مدیر سیستم تماس حاصل فرمایید");
            }
            catch (Exception ex)
            {
                Log.Error(LogMessage.CheckPasswordError, ex);
                throw new UserInterfaceException(
                    "خطای 117: امکان تایید نام کاربری و رمز عبور وجود ندارد. با مدیر سیستم تماس حاصل فرمائید.");
            }
        }

        /// <summary>
        /// بررسی می کند که آیا کاربر دسترسی به صفحه ی مورد نظر دارد یا خیر
        /// </summary>
        /// <param name="user"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public bool HasAccessibility(User user, String page)
        {
            try
            {
                page = page.ToLower();
                Log.Debug("Checking Accessability of UserSSN:'" + user.Username + "' To Page:" + page);
                List<Role> pageRoles = null;
                if (_pagePermissions.Keys.Any(e => e.Equals(page) || e.Contains(page)))
                {
                    pageRoles =
                        _pagePermissions.First(e => e.Key.Equals(page) || e.Key.Contains(page)).Value;
                }
                else
                {
                    Log.Fatal("Access To The Page:'" + page + "' Has Not Been Set");
                    throw new UserInterfaceException("خطای 109:دسترسی صفحه مورد نظر تنظیم نگردیده است" + page);
                }
                if (pageRoles == null)
                {
                    Log.Fatal("Access To The Page:'" + page + "' Has Not Been Set");
                    throw new UserInterfaceException("خطای 103:دسترسی صفحه مورد نظر تنظیم نگردیده است");
                }

                Log.Debug("Successfully Got Roles Of UserSSN:" + user.Username + "'");
                foreach (var userResponsibility in user.Responsibilities)
                {
                    if (pageRoles.Any(r => r.GetRoleType().Equals(userResponsibility.RoleType)))
                    {
                        Log.Debug("UserSSN:'" + user.Username + "' Has Access Permission To Page:" + page);
                        return true;
                    }
                }
                Log.Warn("UserSSN:'" + user.Username + "' Is Trying To Aceess Forbidden Page:'" + page);
                return false;
            }
            catch (SqlException ex)
            {
                Log.Fatal("An Unexpected Error In Checking Access Permission", ex);
                throw new UserInterfaceException(
                    "خطای 104:امکان اتصال سیستم با مرکز اطلاعات فراهم نمی باشد. با مدیر سیستم تماس حاصل فرمایید");
            }
        }

        private IEnumerable<RoleType> GetUserRoles(User user)
        {
            var res = new List<RoleType>();
            try
            {
                foreach (var role in user.Responsibilities)
                {
                    res.Add(role.RoleType);
                }
                return res;
            }
            catch (SqlException ex)
            {
                Log.Fatal("An Unexpected Error In Getting User Roles", ex);
                throw new UserInterfaceException(
                    "خطای 105:امکان اتصال سیستم با مرکز اطلاعات فراهم نمی باشد. با مدیر سیستم تماس حاصل فرمایید");
            }
        }

        public User GetUser(String username)
        {
            return Repository.UserRepository.GetInstance().FindByNationalCode(username);
        }

        public void GoOffline(User user)
        {
            try
            {
                Log.DebugFormat("Trying To LogOut User {0}.", user.Username);
                if (!_onlineUsers.Contains(user))
                {
                    Log.ErrorFormat("User {0} Has Been Logged Out Earlier And Not Deleted From Online Users!",
                        user.Username);
                    return;
                }
                else
                {
                    _onlineUsers.Remove(user);
                    Log.DebugFormat("User {0} LogOut Successfully.", user.Username);
                }

            }
            catch (SqlException ex)
            {
                Log.Fatal(String.Format("An Unexpected Error Occurred In GoOffline User {0}", user.Username), ex);
                throw new UserInterfaceException(
                    "خطای 106:امکان اتصال سیستم با مرکز اطلاعات فراهم نمی باشد. با مدیر سیستم تماس حاصل فرمایید");
            }
        }

        public void ChangeUserPassword(string lastpass, string newpass, string userSSN)
        {
            try
            {
                var user = Repository.UserRepository.GetInstance().FindByNationalCode(userSSN);
                Log.Debug("Trying To Change Password Of UserSSN:'" + userSSN + "'");
                if (!user.Password.Equals(lastpass))
                {
                    Log.Info("Wrong Last Password In Changing Password For UserSSN:'" + userSSN + "'");
                    throw new UserInterfaceException("شناسه عبور قبلی صحیح نمی باشد");
                }
                if (newpass.Length < 6)
                {
                    Log.Info("Wrong Length In Changing Password For UserSSN:'" + userSSN + "'");
                    throw new UserInterfaceException("طول رمز عبور جدید باید حداقل 6 کاراکتر باشد");
                }
                user.ChangePassword(lastpass, newpass);
                Log.Debug("UserSSN:'" + userSSN + "' Changed Password Successfully");
            }
            catch (SqlException ex)
            {
                Log.Fatal("An Unexpected Error In Change Password UserSSN:'" + userSSN + "'", ex);
                throw new UserInterfaceException(
                    "خطای 107:امکان اتصال سیستم با مرکز اطلاعات فراهم نمی باشد. با مدیر سیستم تماس حاصل فرمایید");
            }
        }
        public List<RoleType> CanAssignRoles(User user)
        {
            HashSet<RoleType> result = new HashSet<RoleType>();
            foreach (var responsibility in user.Responsibilities)
            {
                Role role = _allRoles.FirstOrDefault(r => r.GetRoleType().Equals(responsibility.RoleType));
                role.GetCanAssignRole().ForEach(r => result.Add(r));
            }
            return result.ToList();
        }
    }
}