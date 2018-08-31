using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.SqlServer;
using System.Linq;
using PoliceServer.AccessControl;
using PoliceServer.Models;
using PoliceServer.Enums;

namespace PoliceServer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PoliceContext>
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Configuration()
        {
//            this.AutomaticMigrationsEnabled = false;
//            this.AutomaticMigrationDataLossAllowed = false;
//            SetSqlGenerator("System.Data.SqlClient", new SqlMigrator());
        }


        protected override void Seed(PoliceServer.Models.PoliceContext context)
        {
            try
            {

                InitialUser(context);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error("Seed Method Exception: " + ex.Message);
            }
        }

        private static void InitialUser(PoliceContext context)
        {
            User ssh = context.Users.FirstOrDefault(u => u.Username.Equals("administrator"));
            if(ssh == null)
            {
                Log.Debug("User DEFAULT creating...");
                User u = new User
                {
                    Name = "مدیر",
                    Family = "مدیر",
                    Username = "administrator",
                    Password = "administrator",
                    Responsibilities = new List<Responsibility>() {new Responsibility(RoleType.SystemAdmin)}
                };
                context.Users.Add(u);
            }
        }
    }
}
