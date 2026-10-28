using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class PozycjaZamowienia
    {
        //każda pozycja składa się z produktu i jego ilości
        public Produkt Produkt { get; set; }
        public int Ilosc { get; private set; }

        //konstruktor przyjmuje produkt i jego ilość
        public PozycjaZamowienia(Produkt produkt, int ilosc)
        {
            Produkt = produkt;
            Ilosc = ilosc;
        }
        //pomocniczo - wartosc pozycji zamowienia = cena produktu * ilosc
        public double Wartosc => Produkt.Cena * Ilosc;
        //metoda do zwiekszania ilości produktu w zamówieniu
        public void DodajIlosc(int ilosc) => Ilosc += ilosc;
    }
}
