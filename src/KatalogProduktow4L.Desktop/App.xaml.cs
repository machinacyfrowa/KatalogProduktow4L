using System.Configuration;
using System.Data;
using System.Windows;

namespace KatalogProduktow4L.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        { 
            // Tutaj można dodać kod inicjalizacyjny aplikacji
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));
            //teraz reszta kodu inicjalizacyjnego aplikacji
            base.OnStartup(e);
        }
    }

}
