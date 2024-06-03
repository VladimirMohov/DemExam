using DemExam.DataApp;
using DemExam.DataApp.DBModels;
using DemExam.Service.Authorization;
using DemExam.View.Pages.AuthPage;
using DemExam.Views.Models;
using System.Windows;
using System.Windows.Controls;

namespace DemExam.Views.Pages.AuthPage
{
    public partial class SignInPage : Page
    {
        MainWindowViewModel _viewModel;

        public SignInPage(MainWindowViewModel _mainViewModel)
        {
            LoginViewModel loginView = new LoginViewModel();
            InitializeComponent();
            this.DataContext = loginView;
            _viewModel = _mainViewModel;
            _viewModel.Opacity = "0.2";
            _viewModel.ZIndex = "999";
            AppFrame.frame.GetBindingExpression(System.Windows.Controls.Panel.ZIndexProperty).UpdateTarget();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            AuthUserService authUserService = new AuthUserService();
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            bool isLoged = authUserService.AuthenticationUser(login, password);
            if (isLoged)
            {
                User currentUser;
                try
                {
                    currentUser = authUserService.SessionAuthentication();
                }
                catch (Exception)
                {
                    currentUser = null;
                }
                if (currentUser != null)
                {
                    _viewModel.CurrentUser = currentUser;
                }
                _viewModel.Opacity = "1";
                _viewModel.ZIndex = "0";
                AppFrame.frame.GetBindingExpression(System.Windows.Controls.Panel.ZIndexProperty).UpdateTarget();

                this.NavigationService.Content = null;
            }
            else
            {
                MessageBox.Show("Ошибка входа, не правильный логин или пароль", "WARNING!");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new SignUpPage(_viewModel));
        }
    }
}
