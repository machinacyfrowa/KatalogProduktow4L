using KatalogProduktow4L.Core;
using System.Collections.ObjectModel;

namespace KatalogProduktow4L.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly List<Produkt> bazaProduktow = new();
        public ObservableCollection<Produkt> widoczneProdukty { get; set; } = new ObservableCollection<Produkt>();

        public MainPage()
        {
            bazaProduktow = new List<Produkt>
            {
                new Produkt { Nazwa = "Produkt1", Cena = 10.0, Kategoria = "Kategoria1", Ilosc = 5 },
                new Produkt { Nazwa = "Produkt2", Cena = 20.0, Kategoria = "Kategoria2", Ilosc = 3 },
                new Produkt { Nazwa = "Produkt3", Cena = 15.0, Kategoria = "Kategoria1", Ilosc = 8 }
            };
           
            InitializeComponent();
            Odswiez();
            BindingContext = this;
        }
        //przeciążamy metodę OnAppearing, a
        //by odświeżyć widoczne produkty za każdym razem, gdy strona się pojawia
        //nie jest potrzebne od kiedy mamy INotifyPropertyChanged
        protected override void OnAppearing()
        {
            base.OnAppearing(); //najpierw wykonaj działania bazowe, a potem odśwież widoczne produkty
            //Odswiez();
        }
        private void Odswiez()
        {
            widoczneProdukty.Clear();
            string kryteria = nameSearchBar.Text ?? string.Empty;
            List<Produkt> znalezioneProdukty = Produkt.Szukaj(bazaProduktow, kryteria);
            foreach (var produkt in znalezioneProdukty)
            {
                widoczneProdukty.Add(produkt);
            }
            iloscLabel.Text = $"Ilość produktów: {widoczneProdukty.Count}";
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
         Odswiez();
        }
        private void DodajProdukt_Clicked(object sender, EventArgs e)
        {
            //odczytujemy wartości z pól tekstowych
            string nazwa = nazwaEntry.Text;
            double cena = double.Parse(cenaEntry.Text);
            string kategoria = kategoriaPicker.SelectedItem.ToString();

            //tworzymy nową instancję produktu
            Produkt nowyProdukt = new Produkt
            {
                Nazwa = nazwa,
                Cena = cena,
                Kategoria = kategoria,
                Ilosc = 0 // domyślna ilość
            };
            bazaProduktow.Add(nowyProdukt);
            Odswiez();
        }
        private void UsunProdukt_Clicked(object sender, EventArgs e)
        {
            if (productsCollectionView.SelectedItem is Produkt wybranyProdukt)
            {
                bazaProduktow.Remove(wybranyProdukt);
            }
            Odswiez();
        }

        private void nameSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Odswiez();
        }

        private async void SzczegolyButtonClicked(object sender, EventArgs e)
        {
            //sprawdzamy czy mamy wybrany produkt w CollectionView
            if (productsCollectionView.SelectedItem is Produkt wybranyProdukt)
            {
                // Przekazanie wybranego produktu do strony szczegółów
                // przygotowanie paczki z danymi do przekazania
                var dane = new Dictionary<string, object>
                {
                    { "Produkt", wybranyProdukt },
                    { "BazaProduktow", bazaProduktow }
                };
                //wywołanie nawigacji do strony szczegółów z przekazaniem danych
                await Shell.Current.GoToAsync(nameof(DetailsPage), dane);
            }
        }
    }
}
