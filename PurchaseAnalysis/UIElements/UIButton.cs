using System;
using Xamarin.Forms;
namespace PurchaseAnalysis.UIElements
{
    public class UIButton : Button
    {
        public UIButton(string text)
        {
            BackgroundColor = Color.Green;
            TextColor = Color.White;
            Text = text;
        }
    }
}
