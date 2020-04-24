﻿using System;
using PurchaseAnalysis.UIElements;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PurchaseAnalysis
{
    public class MainPage : UIContentPage
    {

        UIButton button = new UIButton("Click to scan code");
        Label text = new Label();
        List<PriceNode> list = new List<PriceNode>();
        //public static decimal sum = 0;
        UIButton1 button1 = new UIButton1("Click to show total sum");
        UILabel1 text1 = new UILabel1("Total: ");
        UIButton buttonNext = new UIButton("Click to show all info");
        string fiscalNumber, fiscalDocument, fiscalSign, n, date;
        public DateTime date1;
        public List<PriceCheck> list1 = new List<PriceCheck>();
        public PriceCheck check;
        //protected internal ObservableCollection<PriceCheck> Checks { get; set; }
        public MainPage()
        {
            button.Clicked += Scan;

            button1.Clicked += Total;

            Update();
            buttonNext.IsVisible = false;
            buttonNext.Clicked += ToCommonPage;
        }

        private async void ToCommonPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommonPage(check));
        }

        void Update()
        {
            StackLayout sl = new StackLayout();

            sl.Children.Add(button);

            foreach (var n in list)
                sl.Children.Add(n);

            sl.Children.Add(button1);
            sl.Children.Add(text1);
            sl.Children.Add(buttonNext);
            buttonNext.IsVisible = true;

            ScrollView scrollView = new ScrollView();
            scrollView.Content = sl;
            this.Content = scrollView;
            Content = scrollView;
        }

        double sum;
        void Total(object sender, EventArgs e)
        {
            try
            {
                sum = 0;
                foreach (var item in list)
                {
                    string str = item.Price.Replace('.', ',');
                    sum += double.Parse(str);

                }
                text1.Text = sum.ToString();
                Update();
            }
            catch (Exception ex) { text1.Text = ex.Message; }
        }
        async void Scan(object sender, EventArgs e)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                text.Text = result;
                int id = list.Count + 1;
                string price = result.Split('=')[2].Split('&')[0];
                list.Add(new PriceNode(id, price));

                string[] result1 = result.Split('&');
                fiscalNumber = result1[2].Remove(0, 2);
                fiscalDocument = result1[3].Remove(0, 2);
                fiscalSign = result1[4].Remove(0, 2);
                n = result1[5].Remove(0, 2);
                date = result1[0].Remove(0, 2);
                date1 = DateTime.Parse(date);


                check = new PriceCheck (sum.ToString(),fiscalNumber, fiscalDocument, fiscalSign,n,date );

                Update();
            }
            catch { }
        }
    }
}
