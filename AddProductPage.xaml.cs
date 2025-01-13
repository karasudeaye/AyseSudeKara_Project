using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AyseSudeKara_Project
{
    public partial class AddProductPage : ContentPage
    {
        public List<Product> Products { get; private set; } 

        private List<Product> products; 
        private List<string> addedProducts; 

        public AddProductPage(List<string> existingAddedProducts)
        {
            InitializeComponent();

            Products = new List<Product>(); 
            addedProducts = existingAddedProducts ?? new List<string>();

            products = new List<Product>
            {
                new Product
                {
                    Name = "The Purest Solutions Toz Peeling",
                    ImageSource = "purest_peeling.jpg",
                    Type = "Peeling",
                    UsageFrequency = "Haftada 1",
                    AdditionalInfo = "Yağlı ve karma ciltler için uygun.",
                    Order = 3
                },
                new Product
                {
                    Name = "CLINIQUE Moisture Surge™ - 100H Auto-Replenishing Nemlendirici",
                    ImageSource = "clinique_moisturizer.jpg",
                    Type = "Nemlendirici Krem",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Tüm cilt tipleri için uygundur.",
                    Order = 7
                },
                new Product
                {
                    Name = "ERBORIAN CC Eye with Centella Asiatica",
                    ImageSource = "erborian_cc_eye.jpg",
                    Type = "Göz Kremi",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Göz çevresi koyuluklarını azaltır.",
                    Order = 6
                },
                new Product
                {
                    Name = "The Purest Solutions Aydınlatıcı C Vitamini Cilt Serumu",
                    ImageSource = "purest_cvitamin.jpg",
                    Type = "C Vitamini Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Cilt tonunu eşitlemeye yardımcı olur.",
                    Order = 5
                },
                new Product
                {
                    Name = "ESTÉE LAUDER Advanced Night Repair - Onarıcı Gece Serumu",
                    ImageSource = "estee_night_repair.jpg",
                    Type = "Gece Serumu",
                    UsageFrequency = "Her gece",
                    AdditionalInfo = "Gece cilt onarımını destekler.",
                    Order = 5
                },
                new Product
                {
                    Name = "Maru-Derm Hyalüronik Asit & Kolajen Cilt Bakım Serumu",
                    ImageSource = "maruderm_hyaluronic.jpg",
                    Type = "Kolajen Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Cilt elastikiyetini artırır.",
                    Order = 5
                },
                new Product
                {
                    Name = "La Roche Posay Effaclar Yüz Temizleme Jeli",
                    ImageSource = "la_roche_posay_cleanser.jpg",
                    Type = "Yüz Yıkama Jeli",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Cilt temizliği için uygun.",
                    Order = 2
                },
                new Product
                {
                    Name = "Garnier Çift Fazlı Micellar Kusursuz Makyaj Temizleme Suyu",
                    ImageSource = "garnier_micellar_water.jpg",
                    Type = "Makyaj Temizleme Suyu",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Makyajı nazikçe temizler.",
                    Order = 1
                },
                new Product
                {
                    Name = "LANCÔME Tonique Confort - Nemlendirici Yüz Toniği",
                    ImageSource = "lancome_tonique_confort.jpg",
                    Type = "Tonik",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Cildi ferahlatır ve hazırlar.",
                    Order = 4
                },
                new Product
                {
                    Name = "La Roche Posay Anthelios UVmune Fluide Invisible SPF 50+ Güneş Koruyucu",
                    ImageSource = "la_roche_posay_sunscreen.jpg",
                    Type = "Güneş Koruyucu",
                    UsageFrequency = "Her gün",
                    AdditionalInfo = "Cilt koruması sağlar.",
                    Order = 8
                },
                new Product
                {
                    Name = "The Purest Solutions Siyah Nokta Ve Sivilce Karşıtı Niacinamide Cilt Bakım Serumu",
                    ImageSource = "purest_niacinamide_serum.jpg",
                    Type = "Niacinamide Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Sivilce ve siyah noktaları azaltır.",
                    Order = 5
                },
                new Product
                {
                    Name = "Cream Co. Hyaluronik Asit Peptit Serum",
                    ImageSource = "cream_co_hyaluronic_serum.jpg",
                    Type = "Hyaluronik Asit Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Cilde nem kazandırır.",
                    Order = 5
                },
                new Product
                {
                    Name = "The Purest Solutions Kırışıklık Karşıtı, Onarıcı Retinol Serum",
                    ImageSource = "purest_retinol_serum.jpg",
                    Type = "Retinol Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Kırışıklık karşıtı ve onarıcı.",
                    Order = 5
                },
                new Product
                {
                    Name = "Licape Gözenek Sıkılaştırıcı ve Cilt Bariyeri Güçlendirici Niacinamide Serum",
                    ImageSource = "licape_niacinamide_serum.jpg",
                    Type = "Niacinamide Serumu",
                    UsageFrequency = "3 günde 1",
                    AdditionalInfo = "Gözenek sıkılaştırıcı etki.",
                    Order = 5
                }
            };

            UpdateProductList(products);
        }

        private void UpdateProductList(IEnumerable<Product> productList)
        {
            var sortedProducts = productList.OrderBy(p => p.Order); 

            ProductList.Children.Clear(); 

            foreach (var product in sortedProducts)
            {
                var productStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5),
                    BackgroundColor = Colors.White,
                    Margin = new Thickness(5, 0) 
                };

                var productImage = new Image
                {
                    Source = product.ImageSource,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    VerticalOptions = LayoutOptions.Center
                };

                var productLabel = new Label
                {
                    Text = product.Name,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 16,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(5, 0)
                };

                var detailsButton = new Button
                {
                    Text = "Detay",
                    BackgroundColor = Colors.Orange,
                    TextColor = Colors.White,
                    FontSize = 12,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(5, 0)
                };

                detailsButton.Clicked += async (s, e) =>
                {
                    var detailPage = new ProductDetailsPage(product);
                    await Navigation.PushAsync(detailPage);
                };

                var addButton = new Button
                {
                    Text = addedProducts.Contains(product.Name) ? "✓" : "+",
                    BackgroundColor = addedProducts.Contains(product.Name) ? Colors.Green : Colors.Purple,
                    TextColor = Colors.White,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(5, 0)
                };

                addButton.Clicked += (s, e) =>
                {
                    if (addedProducts.Contains(product.Name))
                    {
                        addedProducts.Remove(product.Name);
                        addButton.Text = "+";
                        addButton.BackgroundColor = Colors.Purple;
                        SaveButton.IsEnabled = addedProducts.Count > 0;
                    }
                    else
                    {
                        addedProducts.Add(product.Name);
                        addButton.Text = "✓";
                        addButton.BackgroundColor = Colors.Green;
                        SaveButton.IsEnabled = true;
                    }
                };

                var buttonStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                buttonStack.Children.Add(detailsButton);
                buttonStack.Children.Add(addButton);

                productStack.Children.Add(productImage);
                productStack.Children.Add(productLabel);
                productStack.Children.Add(buttonStack);

                ProductList.Children.Add(productStack);
            }
        }



        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var query = e.NewTextValue.ToLower();
            var filteredProducts = products.Where(p => p.Name.ToLower().Contains(query));
            UpdateProductList(filteredProducts);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            foreach (var productName in addedProducts)
            {
                var product = products.FirstOrDefault(p => p.Name == productName);
                if (product != null)
                {
                    Products.Add(product); 
                }
            }

            await DisplayAlert("Başarılı", "Ürünler kaydedildi.", "Tamam");
            await Navigation.PopAsync();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Type { get; set; }
        public string UsageFrequency { get; set; }
        public string AdditionalInfo { get; set; }
        public int Order { get; set; } 
    }
}