using DemExam.Service.Authorization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DemExam.Views.Models
{
    public class LoginViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _login = "";
        private string _password = "";
        private string _email = "";
        private string _loginError = "";
        private string _passwordError = "";
        private string _emailError = "";

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
                ValidateLogin();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ValidatePassword();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ValidateEmail();
            }
        }

        public string LoginError
        {
            get => _loginError;
            set
            {
                _loginError = value;
                OnPropertyChanged();
            }
        }

        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChanged();
            }
        }

        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Login):
                        error = ValidateLogin();
                        break;
                    case nameof(Password):
                        error = ValidatePassword();
                        break;
                    case nameof(Email):
                        error = ValidateEmail();
                        break;
                }
                return error;
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                LoginError = "Логин обязателен";
            }
            else if (Login.Length <= 4)
            {
                LoginError = "Логин должен быть больше 4 символов";
            }
            else if (LoginExists(Login))
            {
                LoginError = "Логин уже существует";
            }
            else
            {
                LoginError = null;
            }
            return LoginError;
        }

        private string ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Пароль обязателен";
            }
            else if (Password.Length <= 4)
            {
                PasswordError = "Пароль должен быть больше 4 символов";
            }
            else
            {
                PasswordError = null;
            }
            return PasswordError;
        }

        private string ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            {
                EmailError = "Некорректная почта";
            }
            else if (Email.Length <= 4)
            {
                EmailError = "Почта должна быть больше 4 символов";
            }
            else if (!Regex.IsMatch(Email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"))
            {
                EmailError = "Почта введена не корректно";
            } else
            {
                EmailError = null;
            }
            return EmailError;
        }

        private bool LoginExists(string login)
        {
            RegUserResiverService service = new RegUserResiverService();
            return service.isExistsUser(login);
        }
    }
}