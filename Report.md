# Inl�mningsuppgift2 ASP.NET Core

_Eventia webbapp, april 2022_<br>
_Pia Hagman_


## Beh�righetshanteringen och struktur

Min webbapp bygger enligt uppgiftens beskrivning p� en enda tabell med anv�ndare. Vilka r�ttigheter (authorizations) den specifika anv�ndaren har 
styrs ist�llet av dess tilldelade roll. Under arbetets g�ng kunde jag ibland fundera kring om det hade varit b�ttre att ha en modell per roll. 
Det hade gett mer utrymme och tydlighet f�r specifika properties tillh�rande den rollen. <br><br> Jag l�ste aldrig min bugg med att s�tta 
`.RequireAuthenticatedUser` i `program.cs` f�r att s�tta ett generellt krav p� inloggning p� samtliga sidor. Risken finns ju p� s� s�tt
att jag missar att s�tta authorization p� n�gon sida. � andra sidan s� beh�vde vi �nd� definera Authorization till en speciell roll p� de flesta
sidor och jag tycker ocks� att det blir tydligt f�r mig sj�lv vad som g�ller n�r det anges p� varje sida. En svaghet �r dock att det �r l�tt att skriva fel n�r man anger
roll i exempelvis `[Authorize(Roles = "organizer")]`. Kanske hade dettta kunnat l�sas med ev variabel ist�llet. 

G�llande strukturen s� tycker jag att det �r lite kostigt att man ska registrera sig f�rst och d� f� rollern "user" och sedan i ett senare steg, under
My Account i mitt fall, "ans�ka" om "organizer"-rollen. Om jag hade byggt appen p� riktigt s� hade jag nog l�tit organisat�rer registrera sig i utloggat l�ge och sedan 
inte tilldelas inloggning f�rr�n admin godk�nt f�rfr�gan. 

Hur sj�lva processen med "ans�kan" till att bli orgnisat�r ska g� till var ocks� n�got vi diskuterade ganska ing�ende. En "mellanstegsroll", en bool p� anv�ndaren 
och en egen tabell f�r ans�kningar var olika alternativ vi funderade kring. Jag valde det sistn�mnda och skapade modellen `Application.cs`d� det k�ndes som att det i det l�ngre perspektivet �r det mest h�llbara.
I dagsl�get �r det ju bara en ans�kan, men modellen skulle om behov finns kunna ut�kas med fler properties som s�ger mer om ans�kan.  

Jag valde ocks� att skapa en service/handler f�r varje roll. Samma tanke d�r - i en utveckling av appen kommer det bli tydligast att dela upp metoderna p� s� s�tt tror jag.

## M�jliga f�rb�ttringar av appen

Som vanligt massa saker som kan f�rb�ttras. N�gra exempel p� utveckling/f�rb�ttring utan inb�rdes ordning:
* Event vars datum har passerats f�rvinner automastiskt (i dagsl�get ville jag inte g�ra det, f�r om jag ska l�gga upp projektet
p� github s� skulle ju eventen f�rsvinna ett efter ett).
* F�rb�ttra anv�ndningen av `_viewImports.cshtml`-filerna. Jag f�rs�kte flytta �ver using-rader, men tyckte inte alltid att det lyckades.
* L�ta organisat�ren ladda upp bilder till sitt event.
* B�ttre felhantering. Jag har lagt in ILogger i mitt projekt, men jag �r inte helt s�ker p� att jag anv�nder det p� ett effektivt s�tt. Jag skulle beh�va bli b�ttre p� att kasta undantag och try/catch-satser.
* m.m.


## Sammantagen k�nsla av projektet

Kul att f� bygga en app som jobbar sig hela v�gen fr�n databas till UI. L�rorikt att f� anv�nda Identity och hur det kan fungera 
med en s�pass etablerad API. Det �r fantastiskt att det finns s� m�nga f�rdiga l�sningar d�rute. K�nns som att en stor del som programmerare
�r att hitta r�tt bland dessa och utnyttja den kod som faktiskt redan finns tillg�nglig p� b�sta s�tt. :)