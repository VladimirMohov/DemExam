using DemExam.DataApp.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExam.Service.Authorization
{
    public class RegUserService
    {
        RegUserResiverService _resiver = new RegUserResiverService();
        public void RegistrateUser(string login, string password, Role role, FullName fullName, string email = null, string phone = null, int? age = null)
        {
            if (_resiver.isExistsUser(login))
            {
                throw new ArgumentException("Логин уже существует");
            }

            _resiver.SaveUser(login, password, role, fullName, email, phone, age);
        }
    }
}
