using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace AyseSudeKara_Project
{
    public partial class AddProductPage : ContentPage
    {
        public delegate void ProductAddedHandler(string productName);
        public event ProductAddedHandler ProductAdded;

        private List<string> availableProducts = new List<string>
        {
            "The Purest Solutions Toz Peeling",
            "CLINIQUE Moisture Surge™ - 100H Auto-Replenishing Nemlendirici",
            "ERBORIAN CC Eye with Centella Asiatica",
            "The Purest Solutions Aydýnlatýcý C Vitamini Cilt Serumu",
            "ESTÉE LAUDER Advanced Night Repair - Onarýcý Gece Serumu",
            "Maru-Derm Hyalüronik Asit & Kolajen Cilt Bakým Serumu"
        };

        public AddProductPage()
        {
            InitializeComponent();
            PopulateProductList();
        }

        private void PopulateProductList()
        {
            foreach (var product in availableProducts)
            {
                var productLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 5 };
                var productLabel = new Label { Text = product, VerticalOptions = LayoutOptions.Center };
                var addButton = new Button { Text = "+" };

                addButton.Clicked += (s, e) =>
                {
                    ProductAdded?.Invoke(product); // Delegate ile ürünü ekleme
                    addButton.Text = "?"; // Ýþaretle
                    addButton.IsEnabled = false; // Tekrar eklemeyi önle
                };

                productLayout.Children.Add(productLabel);
                productLayout.Children.Add(addButton);

                ProductList.Children.Add(productLayout);
            }
        }
    }
}
