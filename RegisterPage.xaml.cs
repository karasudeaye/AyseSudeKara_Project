using Microsoft.Maui.Controls;

namespace AyseSudeKara_Project
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string email = EmailEntry.Text;
            DateTime birthDate = BirthDatePicker.Date;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Hata", "L�tfen t�m alanlar� doldurun.", "Tamam");
                return;
            }

            await DisplayAlert("Ba�ar�l�", "Kay�t ba�ar�l�! Ho� geldiniz.", "Tamam");

            await Navigation.PushAsync(new HomePage());
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
