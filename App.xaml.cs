using Microsoft.Maui.Controls;

namespace AyseSudeKara_Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}