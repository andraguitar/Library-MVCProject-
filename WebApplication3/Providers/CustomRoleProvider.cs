using System;
using System.Linq;
using System.Web.Security;
using DAL.Models;
using Roles = DAL.Models.Roles;

namespace WebApplication3.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly Library1Entities9 _db = new Library1Entities9();
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            Users user = _db.Users.FirstOrDefault(u => u.Name == username);
                if (user != null)
                {
                    Roles userRole = _db.Roles.Find(user.RoleId);
                    if (userRole != null)
                    {
                        roles = new[] { userRole.RoleName };
                    }
                }
            
            return roles;
        }

        public string GetUserName(string username)
        {
            return username;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            Users user = _db.Users.FirstOrDefault(u => u.Name == username);
            if (user != null)
            {
               Roles userRole = _db.Roles.Find(user.RoleId);
               if (userRole != null && userRole.RoleName == roleName)
               outputResult = true;
            }
            
            return outputResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}