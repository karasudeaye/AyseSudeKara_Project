using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace AyseSudeKara_Project
{
    public partial class HomePage : ContentPage
    {
        private DateTime currentDate;
        private List<string> products = new List<string>();

        public HomePage()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
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
        }

        private void OnNextDayClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            UpdateDate();
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Ürün Ekle", "Bu sayfa henüz tasarlanmadý.", "Tamam");
        }

        public void AddProduct(string productName)
        {
            EmptyListLabel.IsVisible = false;

            var productStack = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 5 };
            var checkbox = new CheckBox { HorizontalOptions = LayoutOptions.Start };
            var label = new Label { Text = productName, VerticalOptions = LayoutOptions.Center };

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
