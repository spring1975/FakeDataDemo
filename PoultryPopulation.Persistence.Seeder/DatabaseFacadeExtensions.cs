using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PoultryPopulation.Seeder
{
    internal class CreateUserOptions
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }


    internal static class DatabaseFacadeExtensions
    {
        internal static void EnsureUser(this DatabaseFacade database, 
            CreateUserOptions options)
        {
            EnsureDatabaseUser(database, options);

            if (!string.IsNullOrWhiteSpace(options.Role))
            {
                EnsureRole(database, options);

                AddUserToRole(database, options);
            }
        }

        private static void AddUserToRole(DatabaseFacade database, CreateUserOptions options)
        {
            // NOTE: This script does not like the user and roles passed as parameters via ExecuteSqlInterpolated.
            database.ExecuteSqlRaw($@"
                    IF EXISTS (SELECT name 
                                    FROM [sys].[database_principals]
                                    WHERE name = '{options.Name}')
                    BEGIN
                       IF EXISTS (SELECT is_fixed_role from sys.database_principals where name = '{options.Role}')
                       BEGIN
                    		ALTER ROLE [{options.Role}] ADD MEMBER [{options.Name}]
                       END
                    END");
        }

        private static void EnsureRole(DatabaseFacade database, CreateUserOptions options)
        {
            // NOTE: This script does not like the user and roles passed as parameters via ExecuteSqlInterpolated.
            database.ExecuteSqlRaw($@"
                    IF NOT EXISTS (SELECT is_fixed_role from sys.database_principals where name = '{options.Role}')
                    BEGIN
                       CREATE ROLE {options.Role} AUTHORIZATION [dbo]
                       EXEC sp_addrolemember db_datareader, [{options.Role}]
                       EXEC sp_addrolemember db_datawriter, [{options.Role}]
                    END");
        }

        private static void EnsureDatabaseUser(DatabaseFacade database, CreateUserOptions options)
        {
            // NOTE: This script does not like the user passed as parameters via ExecuteSqlInterpolated.
            database.ExecuteSqlRaw($@"
                IF NOT EXISTS (SELECT name
                               FROM [sys].[database_principals]
                               WHERE name = '{options.Name}')
                BEGIN
                   CREATE USER [{options.Name}] FOR LOGIN [{options.Name}] END");
        }

        /// <summary>
        /// This method must be run against the master database
        /// </summary>
        /// <param name="database"></param>
        /// <param name="options"></param>
        internal static void EnsureServerLogin(this DatabaseFacade database, CreateUserOptions options)
        {
            // NOTE: This script does not like the user and password passed as parameters via ExecuteSqlInterpolated.
            database.ExecuteSqlRaw($@"
                IF NOT EXISTS (SELECT  [name]
                                FROM sys.sql_logins
                                where name = '{options.Name}')
                BEGIN
                	CREATE LOGIN [{options.Name}] WITH PASSWORD = '" + options.Password + "' END");
        }
    }
}
