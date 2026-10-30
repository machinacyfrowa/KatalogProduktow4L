using KatalogProduktow4L.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KatalogProduktow4L.Tests
{
    [TestClass]
    public sealed class Test1
    {
        List<Produkt> PrzygotujListeProduktów()
        {
            Random r = new Random();
            var produkty = new List<Produkt>();
            for (int i = 0; i < 10000; i++)
            {
                Produkt p = new Produkt
                {
                    Nazwa = $"Produkt{i}",
                    Cena = r.NextDouble() * 100,
                    Kategoria = $"Kategoria{r.Next(1, 5)}",
                    Ilosc = r.Next(1, 20)
                };
                produkty.Add(p);
            }
            return produkty;
        }
        Produkt PrzygotujProdukt()
        {
            Random r = new Random();
            Produkt p = new Produkt
            {
                Nazwa = $"ProduktTestowy",
                Cena = r.NextDouble() * 100,
                Kategoria = $"Kategoria{r.Next(1, 5)}",
                Ilosc = r.Next(1, 20)
            };
            return p;
        }
        [TestMethod]
        public void Produkt_SprawdzWartoscMagazynu()
        {
            // Arrange
            var produkt = PrzygotujProdukt();
            double sprawdzonaWartosc = produkt.Cena * produkt.Ilosc;
            // Act
            double wartoscMagazynu = produkt.WartoscMagazynu;
            // Assert
            Assert.AreEqual(sprawdzonaWartosc, wartoscMagazynu, "Wartość magazynu powinna być równa Cena * Ilosc.");
        }
        [TestMethod]
        public void Produkt_CenaUjemna()
        {
            // Arrange
            var produkt = PrzygotujProdukt();
            // Ustawiamy cenę na wartość ujemną
            produkt.Cena *= -1;
            // Act
            bool isCenaDodatnia = produkt.Cena >= 0;
            // Assert
            Assert.IsFalse(isCenaDodatnia, "Cena produktu powinna być nieujemna.");
        }
        [TestMethod]
        public void PoliczWszystkieProdukty()
        {
            // Arrange
            var produkty = PrzygotujListeProduktów();
            // Act
            int liczbaProduktow = produkty.Count;
            // Assert
            Assert.AreEqual(10000, liczbaProduktow, "Liczba produktów powinna wynosić 10000.");
        }
        [TestMethod]
        public void SprawdzCeny()
        {
            // Arrange
            var produkty = PrzygotujListeProduktów();
            // Act
            int licznik = 0;
            foreach(var produkt in produkty)
            {
                if (produkt.Cena > 0)
                {
                    licznik++; // za każdy produkt z ceną większą od 0 zwiększamy licznik
                }
            }
            // Assert czy ilość produktów z ceną jest większą od zera równa całkowitej liczbie produktów
            Assert.AreEqual(produkty.Count, licznik, "Wszystkie produkty powinny mieć cenę większą od 0.");
        }
        //Robimy dwa dodatkowe testy:
        //1. Sprawdzamy czy są dokładnie 3 kategorie produktów w liście
        //2. Sprawdzamy czy w kategorii "Kategoria1" jest dokładnie 5 produktów
        //Najpierw napiszcie sobie funkcję pomocniczą która przygotuje wam dane do testów
        [TestMethod]
        public void CzyOpisPodzespoluZawieraProducenta()
        {
            //arrange - przygotowujemy obiekt podzespołu
            string nazwaProducenta = "Producent1";
            var podzespol = new Podzespol { Nazwa = "Podzespol1", Cena = 25.0, 
                Kategoria = "Kategoria2", Ilosc = 2, Producent = nazwaProducenta };
            //act - sciagam opis do zmiennej
            var opis = podzespol.Opis();
            //assert - sprawdzam czy opis zawiera nazwę producenta
            StringAssert.Contains(opis, nazwaProducenta, "Opis podzespołu powinien zawierać nazwę producenta.");
        }
        //napisz testy które sprawdzą czy:
        //1. wartosc magazynu dla usługi jest poprawnie ustawiona (powinna być równa 0)
        [TestMethod]
        public void WartoscMagazynuDlaUslugi()
        {
            //arrange - przygotowujemy obiekt usługi
            var usluga = new Usluga { Nazwa = "Usluga1", Cena = 20.0, Kategoria = "Kategoria3", Ilosc = 0 };
            //act - sciagam wartosc magazynu do zmiennej
            var wartoscMagazynu = usluga.WartoscMagazynu;
            //assert - sprawdzam czy wartosc magazynu jest równa 0
            Assert.AreEqual(0, wartoscMagazynu, "Wartość magazynu dla usługi powinna być równa 0.");
        }
        //2. czy opis akcesorium zawiera kolor akcesorium
        [TestMethod]
        public void CzyOpisAkcesoriumZawieraKolor()
        {
            //arrange - przygotowujemy obiekt akcesorium
            string kolor = "Czerwony";
            var akcesorium = new Akcesorium
            {
                Nazwa = "Akcesorium1",
                Cena = 15.0,
                Kategoria = "Kategoria2",
                Ilosc = 3,
                Kolor = kolor
            };
            //act - sciagam opis do zmiennej
            var opis = akcesorium.Opis();
            //assert - sprawdzam czy opis zawiera kolor akcesorium
            StringAssert.Contains(opis, kolor, "Opis akcesorium powinien zawierać kolor akcesorium.");
        }

        //testy do napisania 28.10
        //1. puste zamowienie ma sume zamówienia równą 0
        //2. Dodanie dwa razy tego samego produktu do zamówienia nie dodaje pozycji
        //   zwiększa jego ilość i wartość zamówienia jest poprawna 
        //3. Klasa rzuca wyjątek jeśli dodamy do zamówienia produkt z ilością <= 0
        //   wykorzystaj var ex = Assert.Throws<Exception> i sprawdź czy wyjątek jest typu ArgumentException
        //   ex.Message powinien zawierać tekst "Ilość musi być większa od zera."
        [TestMethod]
        public void ZamowienieDodanieUjemnejIlosciRzucaWyjatek()
        {
            // Arrange
            var zamowienie = new Zamowienie();
            var produkt = PrzygotujProdukt();
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() => zamowienie.Dodaj(produkt, -1));
            StringAssert.Contains(ex.Message, "Ilość musi być większa od zera.", "Dodanie produktu z ilością <= 0 powinno rzucić wyjątek ArgumentException.");
        }
        //4. Zamówienie liczy poprawnie sumę wartości dwóch pozycji - jedna z podzespołem, druga z usługą

        //szybki test wyszukiwarki produktów w liście - szukamy produktów po nazwie i sprawdzamy czy wynik jest poprawny
        [TestMethod]
        public void SzukajProduktowPoNazwie()
        {
            // Arrange
            var produkty = PrzygotujListeProduktów();
            string szukanaNazwa = "Produkt1"; // szukamy produktów zawierających "Produkt1" w nazwie
            // Act
            var znalezioneProdukty = Produkt.Szukaj(produkty, szukanaNazwa);
            // Assert
            foreach (var produkt in znalezioneProdukty)
            {
                StringAssert.Contains(produkt.Nazwa, szukanaNazwa, $"Znaleziony produkt {produkt.Nazwa} powinien zawierać '{szukanaNazwa}' w nazwie.");
            }
        }
        //sprawdz ile produktów o określonej nazwie jest w liście i porównaj z wynikiem wyszukiwania
        [TestMethod]
        public void SzukajProduktowPoNazwie_LiczbaZnalezionychProduktow()
        {
            // Arrange
            var produkty = PrzygotujListeProduktów();
            string szukanaNazwa = "000"; // szukamy produktów zawierających "000" w nazwie
            int oczekiwanaLiczbaZnalezionychProduktow = 9; // ponieważ produkty będą miały nazwy Produkt1000, Produkt2000
            // Act
            var znalezioneProdukty = Produkt.Szukaj(produkty, szukanaNazwa);
            // Assert
            Assert.AreEqual(oczekiwanaLiczbaZnalezionychProduktow, znalezioneProdukty.Count, $"Liczba znalezionych produktów powinna być równa {oczekiwanaLiczbaZnalezionychProduktow}.");
        }
    }
}
