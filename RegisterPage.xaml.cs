using Microsoft.Maui.Controls;

namespace AyseSudeKara_Project
{
    public partial class ProductDetailsPage : ContentPage
    {
        public ProductDetailsPage(Product product)
        {
            InitializeComponent();


            ProductNameLabel.Text = product.Name;
            ProductImage.Source = product.ImageSource;
            ProductTypeLabel.Text = $"T�r: {product.Type}";
            ProductUsageLabel.Text = $"Kullan�m S�kl���: {product.UsageFrequency}";
            ProductAdditionalInfoLabel.Text = $"Bilgi: {product.AdditionalInfo}";
        }
    }
}
