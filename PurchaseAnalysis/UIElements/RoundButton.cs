using System;
using Xamarin.Forms;
namespace PurchaseAnalysis.UIElements
{
    public class RoundButton : UIButton
    {
        public RoundButton(string text) : base(text)
        {
            CornerRadius = 50;
            Margin = 20;
            Padding = 10;

        }
    }
}
