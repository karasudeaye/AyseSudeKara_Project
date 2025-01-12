using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AyseSudeKara_Project
{
    public partial class AddProductPage : ContentPage
    {
        public event EventHandler<string> ProductAdded;
        public event EventHandler<string> ProductRemoved;

        private List<Product> products;
        private List<string> addedProducts;

        public AddProductPage(List<string> existingAddedProducts)
        {
            InitializeComponent();

            addedProducts = existingAddedProducts ?? new List<string>();

            products = new List<Product>
            {
                new Product
                {
                    Name = "The Purest Solutions Toz Peeling",
                    ImageSource = "purest_peeling.jpg",
                    Type = "Peeling",
                    UsageFrequency = "Haftada 1",
                    AdditionalInfo = "Yağlı ve karma ciltler için uygun."
                },
                new Product
                {
                    Name = "CLINIQUE Moisture Surge™ - 100H Auto-Replenishing Nemlendirici",
                    ImageSource = "clinique_moisturizer.jpg",
                    Type = "Nemlendirici Krem",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Tüm cilt tipleri için uygundur."
                },
                new Product
                {
                    Name = "ERBORIAN CC Eye with Centella Asiatica",
                    ImageSource = "erborian_cc_eye.jpg",
                    Type = "Göz Kremi",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Göz çevresi koyuluklarını azaltır."
                },
                new Product
                {
                    Name = "The Purest Solutions Aydınlatıcı C Vitamini Cilt Serumu",
                    ImageSource = "purest_cvitamin.jpg",
                    Type = "C Vitamini Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Cilt tonunu eşitlemeye yardımcı olur."
                },
                new Product
                {
                    Name = "ESTÉE LAUDER Advanced Night Repair - Onarıcı Gece Serumu",
                    ImageSource = "estee_night_repair.jpg",
                    Type = "Gece Serumu",
                    UsageFrequency = "Her gece",
                    AdditionalInfo = "Gece cilt onarımını destekler."
                },
                new Product
                {
                    Name = "Maru-Derm Hyalüronik Asit & Kolajen Cilt Bakım Serumu",
                    ImageSource = "maruderm_hyaluronic.jpg",
                    Type = "Kolajen Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Cilt elastikiyetini artırır."
                }
            };

            UpdateProductList(products);
        }

        private void UpdateProductList(IEnumerable<Product> productList)
        {
            ProductList.Children.Clear(); 

            foreach (var product in productList)
            {
                var productStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5)
                };

                var productImage = new Image
                {
                    Source = product.ImageSource,
                    WidthRequest = 50,
                    HeightRequest = 50
                };

                var productLabel = new Label
                {
                    Text = product.Name,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 16,
                    LineBreakMode = LineBreakMode.WordWrap,
                    WidthRequest = 150 
                };

                var detailsButton = new Button
                {
                    Text = "Detay",
                    BackgroundColor = Colors.Orange,
                    TextColor = Colors.White,
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.End
                };

                detailsButton.Clicked += async (s, e) =>
                {
                    var detailPage = new ProductDetailsPage(product);
                    await Navigation.PushAsync(detailPage);
                };

                var addButton = new Button
                {
                    Text = addedProducts.Contains(product.Name) ? "✓" : "+", 
                    BackgroundColor = addedProducts.Contains(product.Name) ? Colors.Green : Colors.Purple,
                    TextColor = Colors.White,
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.End
                };

                addButton.Clicked += (s, e) =>
                {
                    if (addedProducts.Contains(product.Name))
                    {

                        addedProducts.Remove(product.Name);
                        addButton.Text = "+";
                        addButton.BackgroundColor = Colors.Purple;
                        SaveButton.IsEnabled = addedProducts.Count > 0; 
                    }
                    else
                    {

                        addedProducts.Add(product.Name);
                        addButton.Text = "✓";
                        addButton.BackgroundColor = Colors.Green;
                        SaveButton.IsEnabled = true; 
                    }
                };


                var buttonStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                buttonStack.Children.Add(detailsButton);
                buttonStack.Children.Add(addButton);

                productStack.Children.Add(productImage);
                productStack.Children.Add(productLabel);
                productStack.Children.Add(buttonStack);

                ProductList.Children.Add(productStack);
            }
        }




        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var query = e.NewTextValue.ToLower();
            var filteredProducts = products.Where(p => p.Name.ToLower().Contains(query));
            UpdateProductList(filteredProducts);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            foreach (var productName in addedProducts)
            {
                ProductAdded?.Invoke(this, productName);
            }

            foreach (var productName in products.Select(p => p.Name).Where(p => !addedProducts.Contains(p)))
            {
                ProductRemoved?.Invoke(this, productName);
            }

            await DisplayAlert("Başarılı", "Ürünler kaydedildi.", "Tamam");
            await Navigation.PopAsync();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Type { get; set; }
        public string UsageFrequency { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
