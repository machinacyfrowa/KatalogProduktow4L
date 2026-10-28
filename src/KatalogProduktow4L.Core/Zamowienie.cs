using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Zamowienie
    {
        //to jest lista "wewnętrzna" pozycji zamówienia, która nie jest bezpośrednio dostępna z zewnątrz
        private readonly List<PozycjaZamowienia> pozycje = new List<PozycjaZamowienia>();
        //to jest lista "zewnętrzna" pozycji zamówienia, która jest dostępna z zewnątrz, ale nie pozwala na modyfikację
        public IReadOnlyList<PozycjaZamowienia> Pozycje => pozycje;

        //metoda do dodawania pozycji zamówienia
        public void Dodaj(Produkt produkt, int ilosc)
        {
            //najpierw sprawdzamy czy ilość jest poprawna
            if(ilosc <= 0)
                throw new ArgumentException("Ilość musi być większa od zera.");
            //jeśli już istnieje taka pozycja to zwiększamy jej ilość, w przeciwnym razie dodajemy nową pozycję
            var istniejacaPozycja = pozycje.FirstOrDefault(p => p.Produkt == produkt);
            if(istniejacaPozycja != null)
                istniejacaPozycja.DodajIlosc(ilosc);
            else
                pozycje.Add(new PozycjaZamowienia(produkt, ilosc));
        }
        //pomocnicza - suma wartości wszystkich pozycji zamówienia
        public double WartoscZamowienia => pozycje.Sum(p => p.Wartosc);
    }
}
