# Zadanie 3.3 — Katalog na trzech ekranach i plik reguł

**Autor:** Julia Kluga (`estr311a`)  
**Data:** 17.11.2026 r.  
**Narzędzie:** Antigravity IDE (Google DeepMind)  
**Model sztucznej inteligencji:** Gemini 3.8 Flash (High)  
**Technologia:** .NET 9.0 / Avalonia UI (C# / XAML)

---

## 📌 1. Cel i opis projektu

Celem zadania było stworzenie wieloekranowej aplikacji desktopowej **Katalog** opartej o framework Avalonia UI oraz zdefiniowanie zestawu reguł projektowych w pliku `AGENTS.md`, sprawdzając ich respektowanie przez asystenta AI w kolejnych iteracjach.

Aplikacja składa się z trzech ekranów (zrealizowanych jako niezależne kontrolki `UserControl`):
1. **Lista pozycji (`ItemsListView`)** — wczytuje i wyświetla wszystkie elementy katalogu z pliku `dane/pozycje.txt`, umożliwia wybór pozycji, odświeżenie danych oraz przejście do szczegółów lub formularza.
2. **Szczegóły pozycji (`ItemDetailsView`)** — prezentuje szczegółowe informacje o wybranym elemencie: tytuł, rok wydania oraz ocenę (skala 1–10) wraz z przyciskiem powrotu do listy.
3. **Formularz dodawania (`AddItemView`)** — formularz wprowadzania nowej pozycji (tytuł, rok, ocena) z walidacją danych, zapisem w standardzie UTF-8 do pliku `dane/pozycje.txt` oraz natychmiastową aktualizacją widoku listy.

Dodatkowo aplikacja zawiera w oknie głównym (`MainWindow`) pasek nawigacyjny oraz własnoręcznie dodany przycisk **„Policz”**, który zlicza wiersze w pliku danych.

---

## 📋 2. Zasady projektu (`AGENTS.md`)

W pliku `AGENTS.md` umieszczono następujące reguły:
- **Interfejs po polsku, nazwy w kodzie po angielsku.**
- **Jeden ekran to jeden plik `.axaml` i jeden plik `.axaml.cs`.**
- **Bez dodatkowych bibliotek** (wyłącznie czysty .NET 9 i Avalonia UI).
- **Dane wczytujemy z pliku `dane/pozycje.txt`.**
- **Wszystkie pliki w UTF-8.**
- **Nie zmieniaj plików, o które nie było prośby.**
- **Krótkie metody: jedna rzecz, jedna metoda.**
- **Format wiersza danych w pliku: `Tytuł;Rok;Ocena` (rozdzielone średnikiem).**
- **Każdy ekran implementujemy jako `UserControl` ze zdarzeniami nawigacji.**

---

## 🔍 3. Raport z weryfikacji pliku reguł (Pkt 4 i 6)

### A. Liczba złamanych reguł przy pierwszym sprawdzeniu:
- **Liczba złamanych reguł:** **1**
- **Opis:** Podczas pierwszej próby wygenerowania drugiego ekranu agent połączył logikę pobierania zaznaczonego elementu, formatowania tekstu i zmiany widoku w jedną długą metodę event handlera, naruszając zasadę: *„Krótkie metody: jedna rzecz, jedna metoda”*.

### B. Liczba złamanych reguł po poprawieniu AGENTS.md:
- **Liczba złamanych reguł:** **0**
- **Opis:** Po doprecyzowaniu zasad w `AGENTS.md` (wskazanie formatu danych `Tytuł;Rok;Ocena` oraz zasady wydzielania osobnych metod pomocniczych do parsowania, walidacji i nawigacji), asystent wygenerował trzeci ekran w 100% zgodnie z regułami: krótkie, jednozadaniowe metody, pełne nazewnictwo angielskie w C#, polski interfejs użytkownika oraz brak zewnętrznych bibliotek.

### C. Nazwa pliku z zasadami używanego przez edytor:
- W środowisku **Antigravity IDE** standardowym plikiem reguł na poziomie projektu jest **`AGENTS.md`** (oraz `.gemini/rules/*.md` dla reguł kontekstowych).  
- *Dla porównania w Cursor IDE jest to `.cursorrules` / `.cursor/rules/`, a w VS Code (GitHub Copilot) `.github/copilot-instructions.md`.*

### D. Gdzie taki plik powinien się znajdować:
- Plik `AGENTS.md` powinien znajdować się w **głównym folderze projektu (root repository)**, aby środowisko agentowe automatycznie wczytywało go do promptu systemowego każdej nowo otwartej sesji i rozmowy.

### E. Czy edytor obsługuje AGENTS.md:
- **Tak.** Antigravity IDE natywnie obsługuje standard `AGENTS.md`, automatycznie indeksując zawarte w nim reguły jako nadrzędne instrukcje projektowe.

### F. Link do dokumentacji i data sprawdzenia:
- **Link do dokumentacji:** [https://agents.md](https://agents.md) oraz oficjalna dokumentacja reguł projektu w ekosystemie Antigravity / Gemini Code Assist.
- **Data sprawdzenia:** **17.11.2026 r.**

---

## ✋ 4. Własnoręczny przycisk „Policz” (Pkt 5)

Zgodnie z poleceniem dodano przycisk w oknie głównym:
- **W pliku `.axaml`:**
  ```xml
  <Button Click="OnCountClick">Policz</Button>
  <TextBlock Name="CountText" />
  ```
- **W pliku `.axaml.cs`:**
  ```csharp
  private void OnCountClick(object? sender, RoutedEventArgs e)
  {
      var lines = File.ReadAllLines("dane/pozycje.txt");
      CountText.Text = $"Pozycji w pliku: {lines.Length}";
  }
  ```
Po kliknięciu przycisku aplikacja odczytuje plik `dane/pozycje.txt` i wyświetla komunikat np. `Pozycji w pliku: 3`.

---

## ✅ 5. Lista kontrolna zadania 3.3

- [x] Projekt katalogu działa i kompiluje się bez błędów (.NET 9).
- [x] Są trzy ekrany: lista (`ItemsListView`), szczegóły (`ItemDetailsView`) i formularz dodawania (`AddItemView`).
- [x] Dane są przechowywane i odczytywane z `dane/pozycje.txt`.
- [x] W repozytorium znajduje się plik `AGENTS.md`.
- [x] Weryfikacja działania reguł przeprowadzona i udokumentowana.
- [x] Agent przestrzega zasad bez ich przypominania w prompcie.
- [x] Przycisk „Policz” działa poprawnie.
- [x] Zmiany agenta zostały zweryfikowane przed zatwierdzeniem.
- [x] Plik `README.md` zawiera wszystkie wymagane informacje i statystyki.
- [x] Wszystkie etapy udokumentowane w czytelnych commitach git.
