using System;
using Xamarin.Forms;
using System.Runtime.Serialization;
using PurchaseAnalysis.Entities;
namespace PurchaseAnalysis.UIElements
{
    public class Purchase : Frame
    {
        PurchaseEntity entity;

        public Purchase(PurchaseEntity pe)
        {
            entity = pe;
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        TextColor = Color.Green,
                        Text = entity.shop,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    },
                    new Label
                    {
                        TextColor = Color.Green,
                        Text = entity.price.ToString(),
                        HorizontalOptions = LayoutOptions.End
                    },
                }
            };
        }

        public void Tap(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PurchaseInfoPage(entity));
        }
    }
}
