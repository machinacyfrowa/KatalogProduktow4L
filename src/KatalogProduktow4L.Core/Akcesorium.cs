using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Akcesorium : Produkt
    {
        public string Kolor { get; set; }

        public override string Opis()
        {
            return $"Nazwa: {Nazwa}, Cena: {Cena}, Kolor: {Kolor}, Kategoria: {Kategoria}, " +
                $"Ilość: {Ilosc}, Wartość magazynu: {WartoscMagazynu}";
        }
    }
}
