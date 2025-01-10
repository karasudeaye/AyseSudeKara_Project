using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace AyseSudeKara_Project
{
    public partial class AddProductPage : ContentPage
    {
        public event EventHandler<string> ProductAdded; 
        private List<Product> products; 
        private List<string> addedProducts; 

        public AddProductPage(List<string> existingAddedProducts)
        {
            InitializeComponent();
            addedProducts = existingAddedProducts; 
            InitializeProducts(); 
            DisplayProducts(products); 
        }

        private void InitializeProducts()
        {

            products = new List<Product>
            {
                new Product { Name = "The Purest Solutions Toz Peeling", ImageSource = "purest_peeling.jpg" },
                new Product { Name = "CLINIQUE Moisture Surge™ - 100H Auto-Replenishing Nemlendirici", ImageSource = "clinique_moisturizer.jpg" },
                new Product { Name = "ERBORIAN CC Eye with Centella Asiatica", ImageSource = "erborian_cc_eye.jpg" },
                new Product { Name = "The Purest Solutions Aydınlatıcı C Vitamini Cilt Serumu", ImageSource = "purest_cvitamin.jpg" },
                new Product { Name = "ESTÉE LAUDER Advanced Night Repair - Onarıcı Gece Serumu", ImageSource = "estee_night_repair.jpg" },
                new Product { Name = "Maru-Derm Hyalüronik Asit & Kolajen Cilt Bakım Serumu", ImageSource = "maruderm_hyaluronic.jpg" }
            };
        }

        private void DisplayProducts(IEnumerable<Product> productList)
        {
            ProductListLayout.Children.Clear();

            foreach (var product in productList)
            {

                var productRow = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }, 
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, 
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }  
                    },
                    Padding = 10,
                    Margin = new Thickness(0, 5),
                    BackgroundColor = Colors.LightGray
                };


                var productImage = new Image
                {
                    Source = product.ImageSource,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    VerticalOptions = LayoutOptions.Center
                };


                var productName = new Label
                {
                    Text = product.Name,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    LineBreakMode = LineBreakMode.TailTruncation
                };


                var addButton = new Button
                {
                    Text = addedProducts.Contains(product.Name) ? "✓" : "+",
                    BackgroundColor = addedProducts.Contains(product.Name) ? Colors.Green : Colors.LightBlue,
                    WidthRequest = 50,
                    VerticalOptions = LayoutOptions.Center
                };


                addButton.Clicked += (s, e) =>
                {
                    if (addedProducts.Contains(product.Name))
                    {
                        addedProducts.Remove(product.Name);
                        ProductAdded?.Invoke(this, product.Name); 
                        addButton.Text = "+";
                        addButton.BackgroundColor = Colors.LightBlue;
                    }
                    else
                    {
                        addedProducts.Add(product.Name);
                        ProductAdded?.Invoke(this, product.Name); 
                        addButton.Text = "✓";
                        addButton.BackgroundColor = Colors.Green;
                    }
                };


                productRow.Children.Add(productImage, 0, 0);
                productRow.Children.Add(productName, 1, 0);
                productRow.Children.Add(addButton, 2, 0);


                ProductListLayout.Children.Add(productRow);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue?.ToLower() ?? string.Empty;
            var filteredProducts = products.Where(p => p.Name.ToLower().Contains(searchText));
            DisplayProducts(filteredProducts);
        }

        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
