using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Produkt : IWyceniany, IComparable<Produkt>
    {
        public string Nazwa { get; set; }
        public double Cena { get; set; }
        public string Kategoria { get; set; }
        public int Ilosc { get; set; }
        public virtual double WartoscMagazynu
        {
            get { return Cena * Ilosc ; }
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