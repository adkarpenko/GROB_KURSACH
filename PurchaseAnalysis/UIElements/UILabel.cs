using System;
using Xamarin.Forms;
namespace PurchaseAnalysis.UIElements
{
    public class UILabel : Label
    {
        public UILabel(string text)
        {
            TextColor = Color.Green;
            Text = text;
            Margin = 30;
        }
    }
}
