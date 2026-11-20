using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Produkt : IWyceniany, IComparable<Produkt>, INotifyPropertyChanged
    {
        private string nazwa;
        public string Nazwa
        {
            get { return nazwa; }
            set
            {
                if (nazwa != value)
                {
                    nazwa = value;
                    Zmiana(nameof(Nazwa));
                }
            }
        }
        //tu jest faktyczna wartość
        private double cena;
        //to jest właściwość, która wywołuje zdarzenie PropertyChanged, gdy zmienia się wartość ceny
        public double Cena
        {
            get { return cena; } //pobieranie ceny bez dalszych działań - bo to nic nie zmienia
            set
            {
                if (cena != value) //jeśli nowa cena nie jest taka jak poprzednia
                {
                    cena = value; //zapisz nową cenę
                    Zmiana(nameof(Cena));//powiadom, że zmieniła się cena
                }
            }
        }
        public string Kategoria { get; set; }
        public int ilosc;
        public virtual int Ilosc
        {
            get { return ilosc; }
            set
            {
                if (ilosc != value)
                {
                    ilosc = value;
                    Zmiana(nameof(Ilosc));
                }
            }
        }
        private bool dostepnosc;
        public bool Dostepnosc
        {
            get
            {
                return dostepnosc;
            }
            set
            {
                if (dostepnosc != value)
                {
                    dostepnosc = value;
                    Zmiana(nameof(Dostepnosc));
                }
            }
        }
        public virtual double WartoscMagazynu
        {
            get { return Cena * Ilosc ; }
        }
        //publiczna właściwość, która wywołuje metodę Opis
        public string OpisProduktu => Opis();
        //rejestrujemy zdarzenie PropertyChanged, które jest wywoływane,
        //gdy zmienia się wartość właściwości obiektu
        public event PropertyChangedEventHandler? PropertyChanged;
        //funkcja która realnie reaguje na zmianę właściwości obiektu i wywołuje zdarzenie PropertyChanged
        //i triggeruje powiadomienie o zmianie właściwości
        protected void Zmiana(string nazwaWlasciwosci)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWlasciwosci));
        }
        //funkcja sprawdza czy poprawnie obliczamy wartośc magazynu
        public static void SprawdzWartosc(Produkt p)
        {

            if(p.WartoscMagazynu == p.Cena * p.Ilosc)
            {
                Console.WriteLine("Wartość magazynu jest poprawna.");
            }
            else
            {
                Console.WriteLine("Wartość magazynu jest niepoprawna.");
            }
        }
        //funkcja sprawdza czy cena produktu jest poprawna (nieujemna)
        public static void SprawdzCeny(List<Produkt> produkty)
        {
            foreach (var produkt in produkty)
            {
                if (produkt.Cena < 0)
                {
                    Console.WriteLine($"Produkt {produkt.Nazwa} ma niepoprawną cenę: {produkt.Cena}");
                }
            }
        }
        //tu nadpisujemy defaultowa funkcje tostring dla wszystkich obiektow w c#
        public override string ToString()
        {
            return $"Nazwa: {Nazwa}, Cena: {Cena}, Kategoria: {Kategoria}, " +
                $"Ilość: {Ilosc}, Wartość magazynu: {WartoscMagazynu}";
        }
        //dummy funkcja opi stylko zeby pokazac virtual - dopuszczam że ta funkcja zostanie nadpisana w klasach dziedziczących po klasie Produkt
        public virtual string Opis()
        {
            return $"Nazwa: {Nazwa}, Cena: {Cena}, Kategoria: {Kategoria}, " +
                $"Ilość: {Ilosc}, Wartość magazynu: {WartoscMagazynu}";
        }
        /// <summary>
        /// Just for fun implementacja IComparable dla naszego produktu pozwalająca porównywac dwa produkty ze sobą
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Produkt? other)
        {
            //jeśli drugi obiekt jest null, to ten obiekt jest większy (bo 1 > 0)
            if (other == null) return 1;
            //w innym wypadku zwracamy wynik porównania cen produktów
            return this.Cena.CompareTo(other.Cena);
        }
        public static List<Produkt> Szukaj(List<Produkt> produkty, string nazwa)
        {
            List<Produkt> znalezioneProdukty = new List<Produkt>();
            //można pętlą foreach przeszukać listę produktów i dodać do znalezioneProdukty te, które zawierają w nazwie szukany ciąg znaków
            foreach (var produkt in produkty)
            {
                if (produkt.Nazwa.Contains(nazwa, StringComparison.OrdinalIgnoreCase))
                {
                    znalezioneProdukty.Add(produkt);
                }
            }
            //alternatywnie można użyć LINQ do wyszukiwania produktów:
            znalezioneProdukty.Clear(); //czyścimy listę, żeby nie dublować wyników
            znalezioneProdukty = produkty.Where(p => p.Nazwa.Contains(nazwa, StringComparison.OrdinalIgnoreCase)).ToList();
            //zwracamy listę znalezionych produktów
            return znalezioneProdukty;
        }
    }
}