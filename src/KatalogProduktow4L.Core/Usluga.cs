using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Usluga : Produkt
    {
        //usługa nie ma stanu magazynowego wiec
        public override double WartoscMagazynu => 0;
        public override int Ilosc { get => 0; set { } }
        public override string ToString() => Opis(); //zwracamy wynik metody Opis() jako reprezentację tekstową obiektu Usluga
        public override string Opis()
        {
            return $"{Nazwa} (usługa), Cena: {Cena:F2}";
        }
    }
}
