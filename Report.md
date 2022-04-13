# Inlämningsuppgift2 ASP.NET Core

_Eventia webbapp, april 2022_<br>
_Pia Hagman_


## Behörighetshanteringen och struktur

Min webbapp bygger enligt uppgiftens beskrivning på en enda tabell med användare. Vilka rättigheter (authorizations) den specifika användaren har 
styrs istället av dess tilldelade roll. Under arbetets gång kunde jag ibland fundera kring om det hade varit bättre att ha en modell per roll. 
Det hade gett mer utrymme och tydlighet för specifika properties tillhörande den rollen. <br><br> Jag löste aldrig min bugg med att sätta 
`.RequireAuthenticatedUser` i `program.cs` för att sätta ett generellt krav på inloggning på samtliga sidor. Risken finns ju på så sätt
att jag missar att sätta authorization på någon sida. Å andra sidan så behövde vi ändå definera Authorization till en speciell roll på de flesta
sidor och jag tycker också att det blir tydligt för mig själv vad som gäller när det anges på varje sida. En svaghet är dock att det är lätt att skriva fel när man anger
roll i exempelvis `[Authorize(Roles = "organizer")]`. Kanske hade dettta kunnat lösas med ev variabel istället. 

Gällande strukturen så tycker jag att det är lite kostigt att man ska registrera sig först och då få rollern "user" och sedan i ett senare steg, under
My Account i mitt fall, "ansöka" om "organizer"-rollen. Om jag hade byggt appen på riktigt så hade jag nog låtit organisatörer registrera sig i utloggat läge och sedan 
inte tilldelas inloggning förrän admin godkänt förfrågan. 

Hur själva processen med "ansökan" till att bli orgnisatör ska gå till var också något vi diskuterade ganska ingående. En "mellanstegsroll", en bool på användaren 
och en egen tabell för ansökningar var olika alternativ vi funderade kring. Jag valde det sistnämnda och skapade modellen `Application.cs`då det kändes som att det i det längre perspektivet är det mest hållbara.
I dagsläget är det ju bara en ansökan, men modellen skulle om behov finns kunna utökas med fler properties som säger mer om ansökan.  

Jag valde också att skapa en service/handler för varje roll. Samma tanke där - i en utveckling av appen kommer det bli tydligast att dela upp metoderna på så sätt tror jag.

## Möjliga förbättringar av appen

Som vanligt massa saker som kan förbättras. Några exempel på utveckling/förbättring utan inbördes ordning:
* Event vars datum har passerats förvinner automastiskt (i dagsläget ville jag inte göra det, för om jag ska lägga upp projektet
på github så skulle ju eventen försvinna ett efter ett).
* Förbättra användningen av `_viewImports.cshtml`-filerna. Jag försökte flytta över using-rader, men tyckte inte alltid att det lyckades.
* Låta organisatören ladda upp bilder till sitt event.
* Bättre felhantering. Jag har lagt in ILogger i mitt projekt, men jag är inte helt säker på att jag använder det på ett effektivt sätt. Jag skulle behöva bli bättre på att kasta undantag och try/catch-satser.
* m.m.


## Sammantagen känsla av projektet

Kul att få bygga en app som jobbar sig hela vägen från databas till UI. Lärorikt att få använda Identity och hur det kan fungera 
med en såpass etablerad API. Det är fantastiskt att det finns så många färdiga lösningar därute. Känns som att en stor del som programmerare
är att hitta rätt bland dessa och utnyttja den kod som faktiskt redan finns tillgänglig på bästa sätt. :)