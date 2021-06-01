using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace HarwexBank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 0 -> ru-RU
        /// 1 -> en-EN
        /// </summary>
        public static List<CultureInfo> Languages = new();
        public static event Action LanguageChanged;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            Languages.Clear();
            Languages.Add(new CultureInfo("en-EN"));
            Languages.Add(new CultureInfo("ru-RU"));

            try
            {
                ModelResourcesManager.GetInstance();
                
                var app = new ApplicationView { DataContext = new ApplicationViewModel() };
                app.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Environment.Exit(0);
            }
        }

        public static CultureInfo Language
        {
            get => System.Threading.Thread.CurrentThread.CurrentUICulture;
            set
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                var newDictionary = new ResourceDictionary
                {
                    Source = new Uri($"/HarwexBank;component/Languages/lang.{value.Name}.xaml", UriKind.Relative)
                };

                var oldDictionary = (from d in Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("/HarwexBank;component/Languages/lang.")
                    select d).FirstOrDefault();
                if (oldDictionary != null)
                {
                    var newDictionaryId = Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                    Current.Resources.MergedDictionaries.Remove(oldDictionary);
                    Current.Resources.MergedDictionaries.Insert(newDictionaryId, newDictionary);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(newDictionary);
                }
                
                LanguageChanged?.Invoke();
            }
        }
    }
}