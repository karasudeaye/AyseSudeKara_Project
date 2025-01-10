using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace AyseSudeKara_Project
{
    public partial class AddProductPage : ContentPage
    {
        public event EventHandler<string> ProductAdded; 
        private List<Product> products;
        private List<string> addedProducts = new List<string>(); 

        public AddProductPage()
        {
            InitializeComponent();
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
            ProductGrid.Children.Clear();
            ProductGrid.ColumnDefinitions.Clear();


            ProductGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            ProductGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            int row = 0;
            int column = 0;

            foreach (var product in productList)
            {

                var productStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = 10,
                    BackgroundColor = Colors.LightGray,
                    WidthRequest = 150
                };


                var productImage = new Image
                {
                    Source = product.ImageSource,
                    WidthRequest = 100,
                    HeightRequest = 100,
                    HorizontalOptions = LayoutOptions.Center
                };


                var productName = new Label
                {
                    Text = product.Name,
                    FontSize = 14,
                    HorizontalTextAlignment = TextAlignment.Center,
                    LineBreakMode = LineBreakMode.TailTruncation,
                    MaxLines = 2
                };


                var addButton = new Button
                {
                    Text = "+",
                    BackgroundColor = Colors.LightBlue,
                    WidthRequest = 40,
                    HorizontalOptions = LayoutOptions.Center
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


                productStack.Children.Add(productImage);
                productStack.Children.Add(productName);
                productStack.Children.Add(addButton);


                ProductGrid.Children.Add(productStack);
                Grid.SetColumn(productStack, column);
                Grid.SetRow(productStack, row);

                column++;
                if (column > 1) 
                {
                    column = 0;
                    row++;
                }
            }
        }


        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {

            DisplayPromptAsync("Arama", "Ürün adı girin:").ContinueWith(t =>
            {
                if (!string.IsNullOrWhiteSpace(t.Result))
                {
                    var filteredProducts = products.Where(p => p.Name.Contains(t.Result, StringComparison.OrdinalIgnoreCase));
                    MainThread.BeginInvokeOnMainThread(() => DisplayProducts(filteredProducts));
                }
            });
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
