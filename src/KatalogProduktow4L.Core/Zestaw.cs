using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Zestaw : IWyceniany
    {
        private readonly List<Produkt> skladniki;
        public string Nazwa { get; set; }
        /// <summary>
        /// Stwórz nowy zestaw w oparciu o podane produkty
        /// </summary>
        /// <param name="nazwa">Nazwa zestawu</param>
        /// <param name="produkty">Lista produktów</param>
        public Zestaw(string nazwa, List<Produkt> produkty)
        {
            Nazwa = nazwa;
            skladniki = produkty ?? new List<Produkt>();
        }
        /// <summary>
        /// Cena zestawu to suma cen jego składników
        /// </summary>
        public double Cena => skladniki.Sum(p => p.Cena);
    }
}
