using DemExam.DataApp.DBModels;
using DemExam.Service.HashData;
using DemExam.Service.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExam.Service.Authorization
{
    public class AuthUserResiverService
    {
        public string GetHashPassword(string login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (DemexamContext db = new DemexamContext())
            {
                return db.Users.FirstOrDefault(item => item.Login == login).Password;
            }
        }
        public string? GetHashSession(string login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (DemexamContext db = new DemexamContext())
            {
                return db.Users.FirstOrDefault(user => user.Login == login)?.SessionKey;
            }
        }
        public bool SetHashSession(string login, string password)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            HashService hashService = new HashService();
            RegistryService registryService = new RegistryService();

            string genHash = hashService.GenerateHashData(password);

            using (DemexamContext db = new DemexamContext())
            {
                try
                {
                    db.Users.Where(user => user.Login == login).First().SessionKey = genHash;
                    registryService.SetRegistryData(genHash);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        public void RemoveUser(string login)
        {
            using (DemexamContext db = new DemexamContext())
            {
                try
                {
                    User user = db.Users.FirstOrDefault(user => user.Login == login);
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

        }

        public List<User> GetAllUser()
        {
            using (DemexamContext db = new DemexamContext())
            {
                return db.Users.ToList();
            }
        }
        public List<Role> GetAllRole()
        {
            using (DemexamContext db = new DemexamContext())
            {
                return db.Roles.ToList();
            }
        }
        public Role GetRoleByName(string name)
        {
            using (DemexamContext db = new DemexamContext())
            {
                return db.Roles.FirstOrDefault(item => item.RoleName == name);
            }
        }
        public User? GetUserBySessionKey(string sessionKey)
        {
            if (sessionKey == null)
            {
                throw new ArgumentNullException("sessionKey");
            }

            User user;
            try
            {
                using (DemexamContext db = new DemexamContext())
                {
                    user = db.Users.FirstOrDefault(item => item.SessionKey == sessionKey);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return user;
        }

        public Role GetRoleUserByUserId(User user)
        {
            using (DemexamContext db = new DemexamContext())
            {
                return db.Roles.FirstOrDefault(item => item.Id == user.RoleId);
            }
        }
    }
}
