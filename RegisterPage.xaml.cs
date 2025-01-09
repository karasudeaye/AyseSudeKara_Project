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
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
                return;
            }

            await DisplayAlert("Baþarýlý", "Kayýt baþarýlý! Hoþ geldiniz.", "Tamam");

            await Navigation.PushAsync(new HomePage());
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
