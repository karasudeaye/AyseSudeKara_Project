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
            addProductPage.ProductRemoved += OnProductRemoved;

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

        private void OnProductRemoved(object sender, string productName)
        {
            if (toDoProducts.Contains(productName))
            {
                toDoProducts.Remove(productName);
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
                    Spacing = 10,
                    Padding = new Thickness(5)
                };


                var circleButton = new Button
                {
                    Text = "○", 
                    BackgroundColor = Colors.Transparent,
                    BorderColor = Colors.Black,
                    BorderWidth = 1,
                    CornerRadius = 15, 
                    WidthRequest = 30,
                    HeightRequest = 30,
                    FontSize = 14,
                    TextColor = Colors.Black,
                    HorizontalOptions = LayoutOptions.Start
                };

                circleButton.Clicked += (s, e) =>
                {
                    circleButton.Text = circleButton.Text == "○" ? "●" : "○"; 
                };


                var productLabel = new Label
                {
                    Text = product,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14, 
                    LineBreakMode = LineBreakMode.WordWrap, 
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                productStack.Children.Add(circleButton);
                productStack.Children.Add(productLabel);

                ToDoList.Children.Add(productStack);
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateToDoList(); 
        }
    }
}
