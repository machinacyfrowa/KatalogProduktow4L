using KatalogProduktow4L.Core;

namespace KatalogProduktow4L.Mobile;

public partial class DetailsPage : ContentPage, IQueryAttributable
{
    private Produkt? produkt;
    private List<Produkt> baza;
	public DetailsPage()
	{
		InitializeComponent();
    }
	public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query["Produkt"] is not Produkt produkt)
            return;
        this.produkt = produkt;
        if(query["BazaProduktow"] is not List<Produkt> baza)
            return;
        this.baza = baza;
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
            await DisplayAlertAsync("B³¹d", "Nazwa produktu nie mo¿e byæ pusta.", "OK");
            return;
        }
        //je¿eli cena jest niepoprawna (nie jest liczb¹ lub jest ujemna),
        //to nie zapisujemy zmian i wychodzimy z metody
        if (!double.TryParse(CenaEntry.Text, out double cena) || cena < 0)
        {
            await DisplayAlertAsync("B³¹d", "Cena produktu musi byæ liczb¹ nieujemn¹.", "OK");
            return;
        }
        //to samo z iloœci¹
        if (!int.TryParse(IloscEntry.Text, out int ilosc) || ilosc < 0)
        {
            await DisplayAlertAsync("B³¹d", "Iloœæ produktu musi byæ liczb¹ nieujemn¹.", "OK");
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
    private async void UsunButtonClicked(object sender, EventArgs e)
    {
        //czy jesteœ pewien ¿e chcesz usun¹æ produkt?
        bool answer = await DisplayAlertAsync("Potwierdzenie", "Czy na pewno chcesz usun¹æ produkt?", "Tak", "Nie");
        if (!answer)
        {
            return; //przy jakiejkolwiek odpowiedzi "Nie" wychodzimy z metody
        }
        //usuwamy produkt z bazy
        if (produkt is not null && baza is not null)
        {
            baza.Remove(produkt);
        }

        await Shell.Current.GoToAsync("..");
    }
}