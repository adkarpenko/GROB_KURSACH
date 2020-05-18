using System;
using PurchaseAnalysis.UIElements;
using System.Collections.Generic;
using Xamarin.Forms;
using PurchaseAnalysis.Entities;
using System.Web;
namespace PurchaseAnalysis
{
    public class AnalysisPage : UIContentPage
    {

        ScrollView sv = new ScrollView()
        {
            VerticalOptions = LayoutOptions.FillAndExpand
        };
        ScrollView sv1 = new ScrollView()
        {
            VerticalOptions = LayoutOptions.FillAndExpand
        };
        RoundButton rb = new RoundButton("Обновить");

        public AnalysisPage()
        {
            Title = "Аналитика";
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new UITitle("Траты по магазинам"),
                        sv,
                        new UITitle("Траты по категориям"),
                        sv1,
                        rb
                    }
                }
            };
            Update();
            rb.Clicked += (s, e) => Update();
        }

        void Update()
        {
            StackLayout sl = new StackLayout();
            Dictionary<string, double> count = new Dictionary<string, double>();
            foreach (var entity in MainPage.list)
            {
                if (!entity.shop.Contains("?"))
                {
                    if (!count.ContainsKey(entity.shop))
                        count[entity.shop] = 0;
                    count[entity.shop] += entity.price;
                }
            }
            foreach (var key in count.Keys)
            {
                PurchaseEntity pe = new PurchaseEntity
                {
                    shop = key,
                    price = count[key]
                };
                sl.Children.Add(new Purchase(pe));
            }
            string getRequest = "";
            foreach (var key in count.Keys)
                getRequest += "&" + key + "=" + count[key].ToString();
            if (count.Keys.Count > 0)
            {
                getRequest = "?" + getRequest.Substring(1);
                Image image = new Image
                {
                    Source = "http://mathwave.pythonanywhere.com/pie_chart" + getRequest
                };
                sl.Children.Add(image);
            }
            Dictionary<string, double> count2 = new Dictionary<string, double>();
            StackLayout sl2 = new StackLayout();
            foreach(var item in count.Keys)
            {
                if (!item.Contains("?"))
                {
                    if (!count2.ContainsKey(App.shopCategories[item]))
                        count2[App.shopCategories[item]] = 0;
                    count2[App.shopCategories[item]] += count[item];
                }
            }
            foreach (var key in count2.Keys)
            {
                PurchaseEntity pe = new PurchaseEntity
                {
                    shop = key,
                    price = count2[key]
                };
                sl2.Children.Add(new Purchase(pe));
            }
            getRequest = "";
            foreach (var key in count2.Keys)
                getRequest += "&" + key + "=" + count2[key].ToString();
            if (count2.Keys.Count > 0)
            {
                getRequest = "?" + getRequest.Substring(1);
                Image image = new Image
                {
                    Source = "http://mathwave.pythonanywhere.com/pie_chart" + getRequest
                };
                sl2.Children.Add(image);
            }
            sv.Content = sl;
            sv1.Content = sl2;
        }

        
    }
}
