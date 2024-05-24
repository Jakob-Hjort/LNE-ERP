# LNE-ERP - Gruppe3 TECHCOLLEGE 
Jakob hjort, Emil Hytting, Ejnars Leaf

-----------------------------------------------------------------------------

## Velkommen til LNE-ERP README 
I denne README kommmer vi ind på hvordan man bruger ERP-Systemet - 
og hvad du skal vide inden du går igang med at læse vores kode. 

-----------------------------------------------------------------------------

## ERP-Systemet
Hvis ikke du allerede ved hvad et ERP system er, så giver vi dig lidt vide omkring det her - 
Et ERP eller Enterprise Resource Planning, er et system der kan overvåge en virksomhed. Der typisk overvåger - 
**Kunder** , **Kunde Detajler**, **Produkter**, **Produkt Detaljer**, **Virksomheder**, **Virksomheds Detajler**, **Salg**, og **Salgs Detajler**. 

-----------------------------------------------------------------------------

## Projektet 
Vores opgave lød på at programmere et ERP- System til en fiktiv virksomhed som skal indholde nogen forskellige ting såsom - 
Kunder, Produkter, Virksomheder og Salg. Vores interface skal tilknyttetes til en database, hvor der skal laves nogen forskellige tabeller til de rigtige menuer i programmet. 

-----------------------------------------------------------------------------

## TECHCOOL - UI
Programmet har en brugergrænseflade hvor vi bruger TECHCOOL lavet af https://github.com/sinb-dev - 
UI'en er en simpel konsol app med nogen metoder som "listPage.AddCoulmn" ved at få sin liste vist laver man en ListPage, hvor vi så har kaldt den listPage i metoden. 
Derfra kan vi så kalde hvad vi gerne vil have vist på programmets UI. Det kunne for eksempel se sådan her ud `listPage.AddColumn("Fulde navn", nameof(Customer.FullName),20);`

-----------------------------------------------------------------------------
## Mappe Struktur - Og kode
Vi har 6 mapper og gøre med i vores Visual Studio hver mappe undtagen to mapper inholder klasse filer. Her dykker vi lidt ned i hvordan `Company` er stillet op. 
Den første mappe vi ser hedder `Company` mappen indholder `Company.cs`, `CompanyDatabase.cs`, `CompanyDetails.cs`, og `CompanyEditor.cs`.

Company.cs filen indholder en `klasse` som vi kalder `Company`. Inde i `Company` klassen har vi en del `Properties` som vi skal bruge til at kalde nogen oplysninger. -
Her kalder vi fx `CompanyId`, `CompanyName`, `StreetName`, `StreetNumber`, `HouseNumber`, `ZipCode`, `City`, `Country`, `Currency`. Det spænende i det her er så hvordan får vi alt det her vist? - 

I vores `CompanyDatabase.cs` opretter vi en forbindelse til den database vi har fået tildelt i den database skal vi selvfølgelig have nogen tabeller ellers kan vi jo ikke gemme noget..
Koden til at få adgang til det her data vi nu gerne vil have, laver vi en `public List<Company>GetCompanies()` metode. Så bruger vi `using (SqlConnection conn = getConnection())` til at oprette forbindelse til vores database. Vi kan nu også lave en `public void InsertCompany()` metode til at oprette noget data i vores program. Det vigtigt og forstå her vi også snakker sammen med vores UI. Så der laver vi altså en fil der hedder `CompanyListPage.cs` som kan findes under `UI` mappen. og med koden som står i `CompanyListPage.cs` kan vi nu se en UI på programmet når vi starter vores projekt.

-----------------------------------------------------------------------------







