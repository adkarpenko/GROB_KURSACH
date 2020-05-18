using System;
using PurchaseAnalysis.Entities;
using PurchaseAnalysis.UIElements;
using Xamarin.Forms;
namespace PurchaseAnalysis
{
    public class PurchaseInfoPage : UIContentPage
    {
        RoundButton delete = new RoundButton("Удалить");
        PurchaseEntity entity;
        public PurchaseInfoPage(PurchaseEntity p)
        {
            entity = p;
            Content = new StackLayout
            {
                Children =
                {
                    new UITitle("Информация о покупке")
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    new UIInfoLabel("Магазин: " + p.shop),
                    new UIInfoLabel("Категория: " + App.shopCategories[p.shop]),
                    new UIInfoLabel("Расходы: " + p.price.ToString()),
                    new UIInfoLabel("Время:   " + p.dateTime.ToString()),
                    delete
                }
            };
            delete.Clicked += Delete;
        }

        void Delete(object sender, EventArgs e)
        {
            MainPage.list.Remove(entity);
        }
    }
}
