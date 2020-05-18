using System;
using PurchaseAnalysis.UIElements;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using PurchaseAnalysis.Entities;

namespace PurchaseAnalysis
{
    public class MainPage : UIContentPage
    {
        public static PurchaseList list;
        RoundButton ScanButton = new RoundButton("Сканировать код")
        {
            VerticalOptions = LayoutOptions.End
        };

        public MainPage()
        {
            list = new PurchaseList();
            using (MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes((string)App.Current.Properties["purchases"])))
            {
                var formatter = new DataContractJsonSerializer(typeof(PurchaseList));
                list = (PurchaseList)formatter.ReadObject(ms);
            }
            Title = "Покупки";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    list,
                    ScanButton
                }
            };
            ScanButton.Clicked += Scan;
        }

        
        async void Scan(object sender, EventArgs e)
        {
            string res = "";
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                res = await scanner.ScanAsync();
            }
            catch
            {
                return;
            }
            try
            {
                string[] data = res.Split('&');
                double price;
                try
                {
                    string pp = data[1].Split('=')[1];
                    price = double.Parse(pp);
                }
                catch
                {
                    string pp = data[1].Split('=')[1].Replace(".", ",");
                    price = double.Parse(pp);
                }
                string fn = data[2].Split('=')[1];
                if (App.shops.ContainsKey(fn))
                {
                    string shop = App.shops[fn];
                    list.Add(new PurchaseEntity
                    {
                        shop = shop,
                        price = price
                    });
                }
                else
                {
                    await Navigation.PushModalAsync(new AddShopPage(price, fn));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Ошибка чтения QR кода", "Ок");
            }
        }
    }
}
