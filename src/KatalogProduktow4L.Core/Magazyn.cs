using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Magazyn
    {
        public static double WartoscMagazynu(IEnumerable<Produkt> produkty)
        {
            //zwroc sumę sum wartości magazynu wszystkich produktów w magazynie
            return produkty.Sum(p => p.WartoscMagazynu);
        }
    }
}
