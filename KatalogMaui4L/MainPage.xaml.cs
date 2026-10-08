using KatalogProduktow4L;

namespace KatalogMaui4L
{
    public partial class MainPage : ContentPage
    {
        public List<Produkt> Produkty { get; set; }

        public MainPage()
        {
            Produkty = new List<Produkt>
            {
                new Produkt { Nazwa = "Produkt1", Cena = 10.0, Kategoria = "Kategoria1", Ilosc = 5 },
                new Produkt { Nazwa = "Produkt2", Cena = 20.0, Kategoria = "Kategoria2", Ilosc = 3 },
                new Produkt { Nazwa = "Produkt3", Cena = 15.0, Kategoria = "Kategoria1", Ilosc = 8 }
            };
            InitializeComponent();
            BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
