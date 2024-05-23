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
Programmet har en brugergrænseflade hvor vi bruger TECHCOOL lavet af @Hoxer (Indæst hans github her) - 
UI'en er en simpel konsol app med nogen metoder som "listPage.AddCoulmn" ved at få sin liste vist laver man en ListPage, hvor vi så har kaldt den listPage i metoden. 
Derfra kan vi så kalde hvad vi gerne vil have vist på programmets UI. Det kunne for eksempel se sådan her ud `listPage.AddColumn("Fulde navn", nameof(Customer.FullName),20);`

-----------------------------------------------------------------------------


