using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogProduktow4L.Core
{
    /// <summary>
    /// Interfejs wyceniany mówi nam, że każda klasa która go 
    /// implementuje musi posiadać właściwości Nazwa i Cena.
    /// </summary>
    public interface IWyceniany
    {
        string Nazwa { get; }
        double Cena { get; }
    }
}
