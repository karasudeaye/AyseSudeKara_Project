using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AyseSudeKara_Project
{
    public partial class HomePage : ContentPage
    {
        private DateTime currentDate;
        private List<Product> products;
        private Dictionary<string, List<RoutineItem>> dailyRoutineState;

        public HomePage()
        {
            InitializeComponent();
            LoadRoutineState();
            currentDate = DateTime.Now;
            products = new List<Product>(); 
            UpdateDate();
        }

        private void LoadRoutineState()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "routineState.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                dailyRoutineState = JsonSerializer.Deserialize<Dictionary<string, List<RoutineItem>>>(json) ?? new Dictionary<string, List<RoutineItem>>();
            }
            else
            {
                dailyRoutineState = new Dictionary<string, List<RoutineItem>>();
            }
        }

        private void SaveRoutineState()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "routineState.json");
            var json = JsonSerializer.Serialize(dailyRoutineState);
            File.WriteAllText(filePath, json);
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

            var productNames = products.Select(p => p.Name).ToList();


            var addProductPage = new AddProductPage(productNames);
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
            var dateKey = date.ToString("yyyy-MM-dd");

            if (!dailyRoutineState.ContainsKey(dateKey))
            {
                dailyRoutineState[dateKey] = CreateDailyRoutine(date);
            }

            var dailyItems = dailyRoutineState[dateKey];
            ToDoList.Children.Clear();

            if (dailyItems == null || dailyItems.Count == 0)
            {

                var emptyLabel = new Label
                {
                    Text = "Ürün eklemek için + butonuna basın.",
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                ToDoList.Children.Add(emptyLabel);
                return;
            }

            foreach (var item in dailyItems)
            {
                var productStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5)
                };

                var checkBox = new CheckBox
                {
                    IsChecked = item.IsCompleted,
                    VerticalOptions = LayoutOptions.Center,
                    Color = Colors.Green
                };

                var productLabel = new Label
                {
                    Text = item.ProductName,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    TextColor = item.IsCompleted ? Colors.Gray : Colors.Black,
                    TextDecorations = item.IsCompleted ? TextDecorations.Strikethrough : TextDecorations.None
                };

                checkBox.CheckedChanged += (s, e) =>
                {
                    item.IsCompleted = e.Value;
                    productLabel.TextColor = e.Value ? Colors.Gray : Colors.Black;
                    productLabel.TextDecorations = e.Value ? TextDecorations.Strikethrough : TextDecorations.None;
                    SaveRoutineState();
                };

                productStack.Children.Add(checkBox);
                productStack.Children.Add(productLabel);

                ToDoList.Children.Add(productStack);
            }
        }


        private List<RoutineItem> CreateDailyRoutine(DateTime date)
        {
            var routineOrder = new List<string>
            {
                "Makyaj Temizleme Suyu",
                "Yüz Yıkama Jeli",
                "Peeling",
                "Tonik",
                "Cilt ve Yüz Serumları",
                "Göz Çevresi Kremi",
                "Nemlendirici",
                "Güneş Kremi"
            };

            var serumList = products.Where(p => p.Type.Contains("Serum")).ToList();
            var peeling = products.FirstOrDefault(p => p.Type == "Peeling");


            var isPeelingDay = date.DayOfWeek == DayOfWeek.Sunday;

            var currentSerum = serumList.Count > 0 ? serumList[(date.Day - 1) % serumList.Count] : null;

            return routineOrder.Select(step =>
            {
                if (step == "Peeling" && peeling != null)
                {
                    if (isPeelingDay)
                        return new RoutineItem { ProductName = peeling.Name, IsCompleted = false };
                    else
                        return null; 
                }

                if (step == "Cilt ve Yüz Serumları" && currentSerum != null)
                {
                    if (!isPeelingDay)
                        return new RoutineItem { ProductName = currentSerum.Name, IsCompleted = false };
                }

                var product = products.FirstOrDefault(p => p.Type == step);
                return product != null ? new RoutineItem { ProductName = product.Name, IsCompleted = false } : null;
            }).Where(item => item != null).ToList();
        }

    }

    public class RoutineItem
    {
        public string ProductName { get; set; }
        public bool IsCompleted { get; set; }
    }
}