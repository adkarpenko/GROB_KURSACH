using System;
using Xamarin.Forms;
namespace PurchaseAnalysis.UIElements
{
    public class UITitle : UILabel
    {
        public UITitle(string text) : base(text)
        {
            HorizontalOptions = LayoutOptions.Center;
            FontSize = 30;
            FontAttributes = FontAttributes.Bold;
            Margin = 5;
        }
    }
}
