using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using PurchaseAnalysis.Entities;

namespace PurchaseAnalysis
{
    public partial class App : Application
    {
        public static Dictionary<string, string> shops;
        public static List<PurchaseEntity> list;
        public static List<string> categories = new List<string>()
        {
            "Медицина, аптека",
            "Красота и здоровье",
            "Гипермаркет",
            "Бытовая техника",
            "Кафе и рестораны",
            "Транспорт и АЗС",
            "Коммунальные платежи",
            "Штрафы, налоги, комиссии",
            "Одежды и обувь",
            "Хобби и увлечения",
            "Прочие расходы",
            "Развлечения и праздники"
        };
        public static Dictionary<string, string> shopCategories = new Dictionary<string, string>();
        public App()
        {
            InitializeComponent();
            object obj;
            //Current.Properties["purchases"] = "[]";
            //Current.Properties["shops"] = "{}";
            if (!Current.Properties.TryGetValue("purchases", out obj))
                Current.Properties["purchases"] = "[]";
            if (!Current.Properties.TryGetValue("shops", out obj))
                Current.Properties["shops"] = "{}";
            if (!Current.Properties.TryGetValue("categories", out obj))
                Current.Properties["categories"] = "{}";
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes((string)App.Current.Properties["shops"])))
            {
                var formatter = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                shops = (Dictionary<string, string>)formatter.ReadObject(ms);
            }
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes((string)App.Current.Properties["purchases"])))
            {
                var formatter = new DataContractJsonSerializer(typeof(List<PurchaseEntity>));
                list = (List<PurchaseEntity>)formatter.ReadObject(ms);
                list.RemoveAll(item => item.shop.Contains("?"));
            }
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes((string)App.Current.Properties["categories"])))
            {
                var formatter = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                shopCategories = (Dictionary<string, string>)formatter.ReadObject(ms);
            }
            MainPage = new ParentPage();
        }

        public static void SaveContext()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var formatter = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                formatter.WriteObject(ms, shops);
                string newstr = Encoding.UTF8.GetString(ms.ToArray());
                App.Current.Properties["shops"] = newstr;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                var formatter = new DataContractJsonSerializer(typeof(List<PurchaseEntity>));
                formatter.WriteObject(ms, list);
                string newstr = Encoding.UTF8.GetString(ms.ToArray());
                App.Current.Properties["purchases"] = newstr;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                var formatter = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                formatter.WriteObject(ms, shopCategories);
                string newstr = Encoding.UTF8.GetString(ms.ToArray());
                App.Current.Properties["categories"] = newstr;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
