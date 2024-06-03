using DemExam.DataApp.DBModels;
using DemExam.Service.HashData;
using Microsoft.IdentityModel.Tokens;

namespace DemExam.Service.Authorization
{
    public class RegUserResiverService
    {
        public bool isExistsUser(string login)
        {
            try
            {
                using (DemexamContext db = new DemexamContext())
                {
                    return !db.Users.Where(item => item.Login == login).IsNullOrEmpty();
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SaveUser(string login, string password, Role role, FullName fullName, string email = null, string phone = null, int? age = null)
        {
            HashService hashService = new HashService();

            string hashSession = hashService.GenerateHashData(password);
            password = hashService.HashData(password);

            User user = new User()
            {
                RoleId = role.Id,
                Login = login,
                Password = password,
                SessionKey = hashSession,
                DateOfCreation = DateTime.Now,
                Email = email,
                Phone = phone,
                Age = age
            };

            using (DemexamContext db = new DemexamContext())
            {
                try
                {
                    db.FullNames.Add(fullName);
                    db.SaveChanges();
                    user.FullNameId = fullName.Id;

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
    }
}
