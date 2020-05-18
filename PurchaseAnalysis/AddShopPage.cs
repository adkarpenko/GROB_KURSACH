using System;
using PurchaseAnalysis.UIElements;
using PurchaseAnalysis.Entities;
using Xamarin.Forms;
using System.Collections.Generic;
namespace PurchaseAnalysis
{
    public class AddShopPage : UIContentPage
    {

        UIPickerList inUse;
        UIPickerList categories = new UIPickerList(App.categories, false)
        {
            Title = "Категория"
        };
        RoundButton addButton = new RoundButton("Добавить");
        UIEntry shopName = new UIEntry("Новый магазин");
        string fn;
        double price;

        List<string> InUse()
        {
            List<string> res = new List<string>();
            foreach (string item in App.shops.Keys)
                res.Add(App.shops[item]);
            return res;
        }

        public AddShopPage(double price, string fn)
        {
            this.fn = fn;
            this.price = price;
            inUse = new UIPickerList(InUse(), true)
            {
                Title = "Магазин"
            };
            Content = new StackLayout
            {
                Children =
                {
                    new UITitle("Новый магазин"),
                    inUse,
                    categories,
                    shopName,
                    new UILabel("Этот кассовый аппарат еще не был использован. Выберите уже имеющийся магазин или создайте новый.")
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    addButton
                }
            };
            addButton.Clicked += Add;
            inUse.SelectedIndexChanged += OnChange;
        }

        void OnChange(object sender, EventArgs e)
        {
            if (inUse.SelectedIndex == 0)
            {
                shopName.IsVisible = true;
                categories.IsVisible = true;
            }
            else
            {
                shopName.IsVisible = false;
                categories.IsVisible = false;
            }
        }

        void Add(object sender, EventArgs e)
        {
            string name = shopName.Text;
            if (inUse.SelectedIndex == 0)
            {
                if (String.IsNullOrEmpty(name))
                    return;
                if (inUse.Items.Contains(name))
                {
                    DisplayAlert("Ошибка", "Данное имя уже существует", "Ок");
                    return;
                }
            }
            else
                name = inUse.Items[inUse.SelectedIndex];
            App.shops[fn] = name;
            PurchaseEntity pe = new PurchaseEntity
            {
                shop = name,
                price = price
            };
            App.shopCategories[name] = (string)categories.SelectedItem;
            MainPage.list.Add(pe);
            Navigation.PopModalAsync();
        }
    }
}
