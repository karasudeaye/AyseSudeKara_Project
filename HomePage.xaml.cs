using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace AyseSudeKara_Project
{
    public partial class HomePage : ContentPage
    {
        private DateTime currentDate;
        private List<string> toDoProducts;

        public HomePage()
        {
            InitializeComponent();

            currentDate = DateTime.Now;
            toDoProducts = new List<string>();

            UpdateDate();
            UpdateToDoList();
        }

        private void UpdateDate()
        {
            DateLabel.Text = currentDate.ToString("dd MMMM yyyy");
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

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            var addProductPage = new AddProductPage(toDoProducts);

            addProductPage.ProductAdded += OnProductAdded;

            await Navigation.PushAsync(addProductPage);
        }

        private void OnProductAdded(object sender, string productName)
        {
            if (!toDoProducts.Contains(productName))
            {
                toDoProducts.Add(productName);
                UpdateToDoList();
            }
        }

        private void UpdateToDoList()
        {
            ToDoList.Children.Clear();

            if (toDoProducts.Count == 0)
            {
                EmptyListLabel.IsVisible = true;
                return;
            }

            EmptyListLabel.IsVisible = false;

            foreach (var product in toDoProducts)
            {
                var productStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = 5
                };

                var label = new Label
                {
                    Text = product,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 16
                };

                var completeButton = new Button
                {
                    Text = "Tamamla",
                    BackgroundColor = Colors.Green,
                    TextColor = Colors.White,
                    FontSize = 14
                };

                completeButton.Clicked += (s, e) =>
                {
                    label.TextDecorations = TextDecorations.Strikethrough;
                    completeButton.IsEnabled = false;
                };

                productStack.Children.Add(label);
                productStack.Children.Add(completeButton);

                ToDoList.Children.Add(productStack);
            }
        }
    }
}
