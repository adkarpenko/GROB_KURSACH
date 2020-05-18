using System;
using Xamarin.Forms;
using System.Collections.Generic;
namespace PurchaseAnalysis.UIElements
{
    public class UIPickerList : Picker
    {
        public UIPickerList(List<string> items, bool flag)
        {
            Margin = 20;
            TextColor = Color.Green;
            if (flag)
                Items.Add("Новый магазин...");
            foreach (string item in items)
                Items.Add(item);
            SelectedIndex = 0;
        }
    }
}
