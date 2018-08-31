using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PoliceServer.AccessControl;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;
using PoliceServer.Utilities;

namespace PoliceServer.Repository
{
    public class TicketRepository : AbstractRepository<Ticket>
    {
        private TicketRepository(PoliceContext cnt = null) : base(cnt) { }

        public static TicketRepository GetInstance(PoliceContext cnt = null)
        {
            return new TicketRepository(cnt);
        }

        public override Ticket FindById(int id)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), id);
                Ticket ticket = Context.Tickets.FirstOrDefault(t => t.Id == id);
                if (ticket == null)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), id);
                    throw new EntityNotFoundException("بلیط مورد نظر در سیستم ثبت نشده است");
                }
                Log.DebugFormat(LogMessage.FetchByIdFinished, GetName(), id);
                return ticket;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), id), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات بلیط از سیستم بر اساس شناسه");
                }
                throw;
            }
        }

        public void Add(Ticket ticket)
        {

        }

        public override bool Save(Ticket ticket)
        {
            try
            {
                Context.Tickets.Add(ticket);
                Context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Error in saving ticket",e);
                throw;
            }
        }

        public string Save(User u)
        {
            StringBuilder extension = new StringBuilder();
            using (System.Security.Cryptography.MD5 md5 =
                System.Security.Cryptography.MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(DateTime.Now.Ticks.ToString()));
                for (int i = 0; i < retVal.Length; i++)
                {
                    extension.Append(retVal[i].ToString("x2"));
                }
            }

            Ticket ticket = new Ticket()
            {
                UserID = u.Id,
                DateTime = DateTime.Now,
                RoleType = u.GetHighestResponsibility(),
                ExpirationDateTime = DateTime.Now.AddMonths(1),
                Serial = (
                             u.GetHighestResponsibility() == RoleType.SystemAdmin ? "a" :
                             u.GetHighestResponsibility() == RoleType.Doctor ? "d" :
                             u.GetHighestResponsibility() == RoleType.Nurse ? "n" :
                             u.GetHighestResponsibility() == RoleType.Staff ? "s" :
                             "x")
                         + "-"+(extension.ToString().Substring(0,10).ToString())
            };
            TicketRepository.GetInstance().Save(ticket);
            return ticket.Serial;
        }
    }
}