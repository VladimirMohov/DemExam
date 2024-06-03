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
    public class AuthUserService
    {
        AuthUserResiverService _resiver;

        public AuthUserService()
        {
            this._resiver = new AuthUserResiverService();
        }

        public bool AuthenticationUser(string login, string password)
        {
            HashService hashService = new HashService();
            RegistryService registryService = new RegistryService();

            string hashPassword = null;
            try
            {
                hashPassword = _resiver.GetHashPassword(login);
            }
            catch (Exception)
            {
                return false;
            }

            if (hashService.VerifyHashedData(hashPassword, password))
            {
                string? hashSession = _resiver.GetHashSession(login);
                registryService.SetRegistryData(hashSession);
            } else
            {
                return false;
            }

            return true;
        }
        public void RemoveUser(string login)
        {
            _resiver.RemoveUser(login);
        }


        public List<User> GetAllUser()
        {
            return _resiver.GetAllUser();
        }
        public List<Role> GetAllRole()
        {
            return _resiver.GetAllRole();
        }
        public Role GetRoleByName(string name)
        {
            return _resiver.GetRoleByName(name);
        }
        public User? SessionAuthentication()
        {
            RegistryService registryService = new RegistryService();

            string sessionKey;

            try
            {
                sessionKey = registryService.GetRegistryData();
            }
            catch (Exception)
            {
                return null;
            }

            User user = _resiver.GetUserBySessionKey(sessionKey);

            return user;
        }
    }
}