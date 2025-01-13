using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace AyseSudeKara_Project
{
    public partial class HomePage : ContentPage
    {
        private DateTime currentDate;
        private List<Product> products;

        public HomePage()
        {
            InitializeComponent();

            currentDate = DateTime.Now;
            products = new List<Product>(); 
            UpdateDate();
        }

        private void UpdateDate()
        {
            DateLabel.Text = currentDate.ToString("dd MMMM yyyy");
        }

        private void OnPreviousDayClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            UpdateDate();
            UpdateToDoListForDay(currentDate);
        }

        private void OnNextDayClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            UpdateDate();
            UpdateToDoListForDay(currentDate);
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            var addProductPage = new AddProductPage(new List<string>());
            await Navigation.PushAsync(addProductPage);


            addProductPage.Disappearing += (s, args) =>
            {
                if (addProductPage.Products != null && addProductPage.Products.Any())
                {
                    products = addProductPage.Products;
                    UpdateToDoListForDay(currentDate);
                }
            };
        }

        private void UpdateToDoListForDay(DateTime date)
        {
            if (products == null || !products.Any())
            {
                EmptyListLabel.IsVisible = true;
                ToDoList.Children.Clear();
                return;
            }

            EmptyListLabel.IsVisible = false;

            var sortedProducts = products
                .Where(product =>
                {
                    if (product.UsageFrequency == "Her gün") return true;
                    if (product.UsageFrequency == "Haftada 1" && date.DayOfWeek == DayOfWeek.Sunday) return true;
                    if (product.UsageFrequency == "3 günde 1" && (date - DateTime.Now).Days % 3 == 0) return true;
                    return false;
                })
                .OrderBy(product => product.Order);

            ToDoList.Children.Clear();

            foreach (var product in sortedProducts)
            {
                var productStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5)
                };

                var checkBox = new CheckBox
                {
                    Color = Colors.Green,
                    VerticalOptions = LayoutOptions.Center
                };

                var productLabel = new Label
                {
                    Text = product.Name,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                checkBox.CheckedChanged += (s, e) =>
                {
                    productLabel.TextDecorations = e.Value ? TextDecorations.Strikethrough : TextDecorations.None;
                    productLabel.TextColor = e.Value ? Colors.Gray : Colors.Black;
                };

                productStack.Children.Add(checkBox);
                productStack.Children.Add(productLabel);

                ToDoList.Children.Add(productStack);
            }
        }
    }
}
