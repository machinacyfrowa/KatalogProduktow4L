using KatalogProduktow4L.Core;

namespace KatalogProduktow4L.Mobile;

public partial class DetailsPage : ContentPage, IQueryAttributable
{
    private Produkt? produkt;
	public DetailsPage()
	{
		InitializeComponent();
    }
	public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query["Produkt"] is not Produkt produkt)
            return;
        this.produkt = produkt;
        BindingContext = produkt;
        NazwaEntry.Text = produkt.Nazwa;
        CenaEntry.Text = produkt.Cena.ToString();
        IloscEntry.Text = produkt.Ilosc.ToString();
        //KategoriaPicker.SelectedItem = produkt.Kategoria;
    }

    private async void PowrotButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    private async void ZapiszButtonClicked(object sender, EventArgs e)
    {
        //je¿eli nazwa jest pusta, to nie zapisujemy zmian i wychodzimy z metody
        if (String.IsNullOrWhiteSpace(NazwaEntry.Text))
        {
            return;
        }
        //je¿eli cena jest niepoprawna (nie jest liczb¹ lub jest ujemna),
        //to nie zapisujemy zmian i wychodzimy z metody
        if (!double.TryParse(CenaEntry.Text, out double cena) || cena < 0)
        {
            return;
        }
        //to samo z iloœci¹
        if (!int.TryParse(IloscEntry.Text, out int ilosc) || ilosc < 0)
        {
            return;
        }
        if(produkt is null)
        {
            return;
        }
        //wszystko sprawdziliœmy, wiêc zapisujemy zmiany w obiekcie Produkt
        produkt.Nazwa = NazwaEntry.Text;
        produkt.Cena = cena;
        produkt.Ilosc = ilosc;
        await Shell.Current.GoToAsync("..");
    }
}