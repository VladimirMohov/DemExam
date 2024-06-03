using DemExam.DataApp;
using DemExam.DataApp.DBModels;
using DemExam.Service.Authorization;
using DemExam.Views.Models;
using DemExam.Views.Pages.AuthPage;
using System.Windows;
using System.Windows.Controls;

namespace DemExam.View.Pages.AuthPage
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        MainWindowViewModel _viewMainModel;
        LoginViewModel _viewLoginModel;
        public SignUpPage(MainWindowViewModel _mainViewModel)
        {
            InitializeComponent();
            _viewLoginModel = new LoginViewModel();
            _viewMainModel = _mainViewModel;
            _viewMainModel.Opacity = "0.2";
            _viewMainModel.ZIndex = "999";
            AppFrame.frame.GetBindingExpression(System.Windows.Controls.Panel.ZIndexProperty).UpdateTarget();
            this.DataContext = _viewLoginModel;
        }

        private void ConfirmRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegUserService service = new RegUserService();
            AuthUserService authService = new AuthUserService();
            _viewLoginModel.Login = LoginBox.Text;
            _viewLoginModel.Password = PasswordBox.Password;
            _viewLoginModel.Email = EmailBox.Text;

            if (_viewLoginModel.Login.Length < 4 || _viewLoginModel.Password.Length < 4 || _viewLoginModel.Email.Length < 4)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "WARNING");
                return;
            }
            if (_viewLoginModel.LoginError != null || _viewLoginModel.EmailError != null || _viewLoginModel.PasswordError != null)
            {
                return;
            }

            FullName name = new FullName()
            {
                FirstName = "",
                SecondName = "",
                Patronymic = ""
            };
            service.RegistrateUser(_viewLoginModel.Login, _viewLoginModel.Password, authService.GetRoleByName("Студент"), name, _viewLoginModel.Email);

            SignInPage signInPage = new SignInPage(_viewMainModel);
            AppFrame.frame.Navigate(signInPage);
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            SignInPage signInPage = new SignInPage(_viewMainModel);
            AppFrame.frame.Navigate(signInPage);
        }
    }
}
