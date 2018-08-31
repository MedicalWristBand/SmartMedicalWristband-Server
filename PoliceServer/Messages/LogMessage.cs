using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceServer.Messages
{
    public class LogMessage
    {
        #region FindByUsername
        public const String FetchByUsernameBegin = "Try To Fetch {0} By Username {1}.";
        public const String FetchByUsernameFinished = "Fetching {0} By Username {1} Finished Successfully.";
        public const String FetchByUsernameNotFound = "Try To Fetch {0} By Username {1} Which Was Not Exist!";
        public const String FetchByUsernameError = "Error Occurred While Fetching {0} By Username {1}!";
        #endregion FindByUsername

        #region SimplePage
        public static readonly String PageInitBegin = "Try To Init Page {0}";
        public static readonly String PageLoadBegin = "Try To Load Page {0}";
        public static readonly String PageLoadFinish = "Loading Page {0} Finished Successfully";
        #endregion SimplePage

        #region FindById
        public const String FetchByIdBegin = "Try To Fetch {0} By Id {1}.";
        public const String FetchByIdFinished = "Fetching {0} By Id {1} Finished Successfully.";
        public const String FetchByIdNotFound = "Try To Fetch {0} By Id {1} Which Was Not Exist!";
        public const String FetchByIdError = "Error Occurred While Fetching {0} By Id {1}!";
        #endregion FindById

        #region user
        public static readonly String LoadPageByIP = "User With IP {0} Try To Load Page {1}";
        public static readonly String LoginBegin = "A Client by iP {0} And Username {1} Tried To Login";
        public static readonly String LoginFinished = "User By Username {0} Logged In Successfuly.";
        public static readonly String LoginError = "Error Occurred While Logging in By Name Usrename {0}!";
        #endregion user

        #region RoleManager
        public const String CheckPasswordBegin = "Try To Check Password For Username {0}.";
        public const String CheckPasswordFinished = "Check Password For Username {0} Finished Successfully.";
        public const String CheckPasswordFalse = "Password Entered For Username {0} Was Not Accepted By System.";
        public const String CheckPasswordTrue = "Password Entered For Username {0} Is True.";
        public const String CheckPasswordSqlException = "Cannot Retrive Data From Database.";
        public const String CheckPasswordError = "Checking Password Stopped Suddenly!";
        public const String CheckPasswordWarning = "UserInterfaceException Throwed In CheckPassword";
        #endregion

        #region FindByName
        public const String FetchByNameBegin = "Try To Fetch {0} By Name {1}.";
        public const String FetchByNameFinished = "Fetching {0} By Name {1} Finished Successfully.";
        public const String FetchByNameNotFound = "Try To Fetch {0} By Name {1} Which Was Not Exist!";
        public const String FetchByNameError = "Error Occurred While Fetching {0} By Name {1}!";
        #endregion FindByName

        #region FindBySerial
        public const String FetchBySerialBegin = "Try To Fetch {0} By Serial {1}.";
        public const String FetchBySerialFinished = "Fetching {0} By Serial {1} Finished Successfully.";
        public const String FetchBySerialNotFound = "Try To Fetch {0} By Serial {1} Which Was Not Exist!";
        public const String FetchBySerialError = "Error Occurred While Fetching {0} By Serial {1}!";
        #endregion FindBySerial

        #region FetchAll
        public const String FetchAllBegin = "Try to Fetch all {0} Begins.";
        public const String FetchAllFinished = "Fetching all {0} Finished Successfully.";
        public const String FetchAllError = "Error Occurred While Fetching all {0}!";
        public const String FetchAllNotFound = "No {0} Found!";
        #endregion FetchAll

        #region FindByNationalCode
        public const String FetchByNationalCodeBegin = "Try To Fetch {0} By NationalCode {1}.";
        public const String FetchByNationalCodeFinished = "Fetching {0} By NationalCode {1} Finished Successfully.";
        public const String FetchByNationalCodeNotFound = "Try To Fetch {0} By NationalCode {1} Which Was Not Exist!";
        public const String FetchByNationalCodeError = "Error Occurred While Fetching {0} By NationalCode {1}!";
        #endregion FindByNationalCode

        #region SaveObject
        public const String SaveObjectBegin = "Try To Save {0} By Specific ID of {1}.";
        public const String SaveObjectFinished = "Saving {0} By Specific ID {1} Finished Successfully.";
        public const String SaveObjectRedundantStoped = "Saving {0} By Specific ID {1} Stoped because it was REDUNDANT.";
        public const String SaveObjectError = "Error Occurred While Saving {0} By Specific ID {1}!";
        #endregion SaveObject


    }
}