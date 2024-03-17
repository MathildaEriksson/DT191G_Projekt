# EquiMarket - Projekt DT191G - Mathilda Eriksson

Publicerad på [EquiMarket](https://equimarket.azurewebsites.net/)

EquiMarket är en webbplattform designad för att förenkla köp och försäljning av hästar. Plattformen erbjuder en användarvänlig miljö där användare kan skapa, bläddra och interagera med annonser.

## Funktioner

- **Annonser:** Användare kan skapa annonser för att sälja eller köpa hästar.
- **Sökfunktionalitet:** Möjliggör filtrering och sökning bland annonser baserat på olika kriterier.
- **Meddelandesystem:** En funktion som tillåter användare att kommunicera direkt med varandra.
- **Användarhantering:** Stöd för registrering, inloggning och användarprofiler.
- **Adminfunktionalitet:** Administratörer kan hantera annonser på plattformen.

## Teknisk stack

- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- Tailwind CSS för frontend-design

## Installation och körning

För att köra EquiMarket lokalt, följ dessa steg:

### 1. Klona repot till din lokala maskin:

``` git clone https://github.com/MathildaEriksson/DT191G_Projekt.git ```

### 2. Navigera till projektets katalog och återställ NuGet-paket:

``` cd EquiMarket ```

``` dotnet restore ```

### 3. Kör migrations för att skapa databasen:

``` dotnet ef database update ```

### 4. Starta applikationen:
``` dotnet run ```

Besök `http://localhost:5000` i din webbläsare för att använda applikationen.
