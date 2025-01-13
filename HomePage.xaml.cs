using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

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
            dailyRoutineState = new Dictionary<string, List<RoutineItem>>();
            currentDate = DateTime.Now;

            products = new List<Product>();
            UpdateDate();
            UpdateToDoListForDay(currentDate);
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateToDoListForDay(currentDate);
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            var addProductPage = new AddProductPage(new List<string>());

            addProductPage.ProductAdded += async (s, product) =>
            {
                Console.WriteLine($"Yeni ürün eklendi: {product.Name}");

                for (var date = currentDate; date <= currentDate.AddDays(30); date = date.AddDays(1))
                {
                    var dateKey = date.ToString("yyyy-MM-dd");

                    if (!dailyRoutineState.ContainsKey(dateKey))
                    {
                        dailyRoutineState[dateKey] = CreateDailyRoutine(date);
                    }


                    if (!dailyRoutineState[dateKey].Any(r => r.ProductName == product.Name))
                    {
                        dailyRoutineState[dateKey].Add(new RoutineItem
                        {
                            ProductName = product.Name,
                            IsCompleted = false
                        });
                    }

                    await Task.Delay(10); 
                }
            };

            await Navigation.PushAsync(addProductPage);
            UpdateToDoListForDay(currentDate);
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
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(70, 40, 0, 0),
                    TextColor = Colors.Black
                };
                ToDoList.Children.Add(emptyLabel);
                return;
            }

            foreach (var item in dailyItems.OrderBy(i => i.Order)) 
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
        "Serum",
        "Göz Çevresi Kremi",
        "Nemlendirici",
        "Güneş Kremi"
    };

            var dailyRoutine = new List<RoutineItem>();
            var serumList = products.Where(p => p.Type == "Serum").ToList();
            var peeling = products.FirstOrDefault(p => p.Type == "Peeling");

            bool isPeelingDay = (date.DayOfWeek == DayOfWeek.Sunday); 

            RoutineItem serumItem = null;
            if (serumList.Count > 0 && !isPeelingDay)
            {
                var serumIndex = ((date.Day - 1) / 3) % serumList.Count; 
                var currentSerum = serumList[serumIndex];
                serumItem = new RoutineItem
                {
                    ProductName = currentSerum.Name,
                    IsCompleted = false,
                    Order = 5 
                };
            }

            foreach (var step in routineOrder)
            {
                if (step == "Peeling")
                {
                    if (isPeelingDay && peeling != null)
                    {
                        dailyRoutine.Add(new RoutineItem
                        {
                            ProductName = peeling.Name,
                            IsCompleted = false,
                            Order = 3 
                        });
                    }
                    continue;
                }

                if (step == "Serum")
                {
                    if (serumItem != null)
                    {
                        dailyRoutine.Add(serumItem);
                    }
                    continue;
                }

                var product = products.FirstOrDefault(p => p.Type == step && ShouldIncludeProduct(date, p));
                if (product != null)
                {
                    dailyRoutine.Add(new RoutineItem
                    {
                        ProductName = product.Name,
                        IsCompleted = false,
                        Order = routineOrder.IndexOf(step) + 1 
                    });
                }
            }

            return dailyRoutine;
        }


        private bool ShouldIncludeProduct(DateTime date, Product product)
        {
            switch (product.UsageFrequency)
            {
                case "Her gün":
                    return true;

                case "Haftada 1":
                    return date.DayOfWeek == DayOfWeek.Sunday;

                case "3 günde 1":
                    return (date.Day - 1) % 3 == 0;

                default:
                    return false; 
            }
        }



    }

    public class RoutineItem
    {
        public string ProductName { get; set; }
        public bool IsCompleted { get; set; }
        public int Order { get; set; } 

    }
}
