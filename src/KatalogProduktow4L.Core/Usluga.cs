using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    public class Usluga : Produkt
    {
        //usługa nie ma stanu magazynowego wiec
        public override double WartoscMagazynu => 0;

        public override string Opis()
        {
            return $"{Nazwa} (usługa), Cena: {Cena:F2}";
        }
    }
}
