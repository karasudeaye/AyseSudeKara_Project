using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace AyseSudeKara_Project
{
    public partial class HomePage : ContentPage
    {
        private List<string> products = new List<string>();
        private DateTime currentDate;


        public HomePage()
        {
            InitializeComponent();
            UpdateDate();
        }

        private void UpdateDate()
        {
            DateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {

            var addProductPage = new AddProductPage();
            addProductPage.ProductAdded += (s, productName) =>
            {
                if (!products.Contains(productName))
                {
                    products.Add(productName);
                    UpdateProductList();
                }
            };

            await Navigation.PushAsync(addProductPage);
        }

        private void UpdateProductList()
        {
            ToDoList.Children.Clear();

            if (products.Count == 0)
            {
                EmptyListLabel.IsVisible = true;
            }
            else
            {
                EmptyListLabel.IsVisible = false;
                foreach (var product in products)
                {
                    var productStack = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 5 };
                    var checkbox = new CheckBox { HorizontalOptions = LayoutOptions.Start };
                    var label = new Label { Text = product, VerticalOptions = LayoutOptions.Center };

                    checkbox.CheckedChanged += (s, args) =>
                    {
                        if (args.Value)
                        {
                            label.TextDecorations = TextDecorations.Strikethrough;
                        }
                        else
                        {
                            label.TextDecorations = TextDecorations.None;
                        }
                    };

                    productStack.Children.Add(checkbox);
                    productStack.Children.Add(label);

                    ToDoList.Children.Add(productStack);
                }
            }
        }

        private void OnPreviousDayClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            UpdateDate();
        }

        private void OnNextDayClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            UpdateDate();
        }

    }
}
