using KatalogProduktow4L.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace KatalogProduktow4L.Mobile
{
    internal class MagazynPlikowy
    {
        /// <summary>
        /// Generuje pełną ścieżkę do pliku JSON w katalogu danych aplikacji.
        /// </summary>
        private static string sciezkaDoPliku =>
            Path.Combine(FileSystem.Current.AppDataDirectory, "bazaProduktow.json");
        /// <summary>
        /// Asynchronicznie zapisuje produkty jako sformatowany JSON.
        /// </summary>
        /// <param name="produkty">Lista produktów do zapisania.</param>
        /// <returns>Zadanie reprezentujące asynchroniczną operację zapisu.</returns>
        public static async Task ZapiszAsync(List<Produkt> produkty)
        {
            string json = JsonSerializer.Serialize(produkty,
                                            // to jest opcjonalne, ale ładnie formatuje JSON z wcięciami
                                            // zamiast tego macie cały tekst ciągiem
                                            new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(sciezkaDoPliku, json);
        }
        /// <summary>
        /// Wczytuje produkty z pliku JSON.
        /// </summary>
        /// <remarks>Błędy odczytu pliku i deserializacji nie są obsługiwane i są przekazywane
        /// wywołującemu.</remarks>
        /// <returns>Listę wczytanych produktów albo pustą listę, jeśli plik nie istnieje lub jego zawartość ma wartość null.</returns>
        public static async Task<List<Produkt>> WczytajAsync()
        {
            //jeżeli nie ma pliku to zwróć pustą listę produktów
            if (!File.Exists(sciezkaDoPliku))
            {
                return new List<Produkt>(); //tu kończy się metoda, bo nie ma pliku
            }
            //tu dojdziemy tylko jeśli plik istnieje, więc możemy go wczytać
            // w pełni świadomie pomijamy obsługę wyjątków żeby nie komplikować
            string json = await File.ReadAllTextAsync(sciezkaDoPliku);
            return JsonSerializer.Deserialize<List<Produkt>>(json) ?? new List<Produkt>();
        }
    }
}
