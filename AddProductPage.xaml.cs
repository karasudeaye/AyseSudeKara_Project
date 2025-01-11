using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AyseSudeKara_Project
{
    public partial class AddProductPage : ContentPage
    {
        public event EventHandler<string> ProductAdded;

        private List<Product> products;
        private List<string> addedProducts;
        private List<string> toDoProducts;

        public AddProductPage(List<string> existingAddedProducts)
        {
            InitializeComponent();

            toDoProducts = existingAddedProducts ?? new List<string>();
            addedProducts = new List<string>();

            products = new List<Product>
            {
                new Product { Name = "The Purest Solutions Toz Peeling", ImageSource = "purest_peeling.jpg" },
                new Product { Name = "CLINIQUE Moisture Surge™ - 100H Auto-Replenishing Nemlendirici", ImageSource = "clinique_moisturizer.jpg" },
                new Product { Name = "ERBORIAN CC Eye with Centella Asiatica", ImageSource = "erborian_cc_eye.jpg" },
                new Product { Name = "The Purest Solutions Aydınlatıcı C Vitamini Cilt Serumu", ImageSource = "purest_cvitamin.jpg" },
                new Product { Name = "ESTÉE LAUDER Advanced Night Repair - Onarıcı Gece Serumu", ImageSource = "estee_night_repair.jpg" },
                new Product { Name = "Maru-Derm Hyalüronik Asit & Kolajen Cilt Bakım Serumu", ImageSource = "maruderm_hyaluronic.jpg" }
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
                    WidthRequest = 250
                };

                var addButton = new Button
                {
                    Text = addedProducts.Contains(product.Name) ? "✓" : "+",
                    BackgroundColor = addedProducts.Contains(product.Name) ? Colors.Green : Colors.Purple,
                    TextColor = Colors.White
                };

                addButton.Clicked += (s, e) =>
                {
                    if (addedProducts.Contains(product.Name))
                    {
                        addedProducts.Remove(product.Name);
                        addButton.Text = "+";
                        addButton.BackgroundColor = Colors.Purple;
                    }
                    else
                    {
                        addedProducts.Add(product.Name);
                        addButton.Text = "✓";
                        addButton.BackgroundColor = Colors.Green;
                    }
                };

                productStack.Children.Add(productImage);
                productStack.Children.Add(productLabel);
                productStack.Children.Add(addButton);

                ProductList.Children.Add(productStack);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var query = e.NewTextValue.ToLower();
            var filteredProducts = products.Where(p => p.Name.ToLower().Contains(query));
            UpdateProductList(filteredProducts);
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            foreach (var productName in addedProducts)
            {
                if (!toDoProducts.Contains(productName))
                {
                    toDoProducts.Add(productName);
                }
            }


            foreach (var productName in addedProducts)
            {
                ProductAdded?.Invoke(this, productName);
            }


            Navigation.PopAsync();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
