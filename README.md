# Azure alapú webhoszting

## Célkitűzés
Adatbázist használó webalkalmazás telepítése Azure környezetbe. Azure menedzsment eszközök megismerése, pl. Azure portál, Azure Cloud Shell. Azure erőforrások létrehozása és konfigurálása. ADO.NET alapú adatelérés (az önálló részben).

## Előfeltételek

A labor elvégzéséhez szükséges eszközök:

- parancssori eszközök
  - .NET 8 SDK (Visual Studio 2022 általában telepíti) - [telepítési útmutató](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
  - EF Core Tools v8 vagy későbbi - [telepítési útmutató](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-tools)
  - Azure CLI - [telepítési útmutató](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- Windows-on terminálnak ajánlott a [Windows Terminal](https://github.com/microsoft/terminal) használata (Windows 11-ben ez az alapértelmezett terminál) vagy a Visual Studio Code beépített [terminálja](https://code.visualstudio.com/docs/terminal/basics)
- PowerShell 7 parancsértelmező - [telepítési útmutató](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.3)
- böngésző

A labor alapvetően cross-platform (pl. bizonyos linux disztribúciókon is elvégezhető), azonban a leírás néhány helyen Windows-specifikus (pl. elérési útvonalak megadása).

Amit érdemes átnézned:

- Azure előadások anyaga

## Gyakorlat menete

A közös rész és az önálló rész gyakorlatilag független, bármilyen sorrendben elvégezhető.

## Közös rész

### Előkészítés - Előfizetés

Használhatjuk az évente megújítható hallgatói előfizetést vagy egy rövid életű, korlátozott sandbox előfizetést is.

#### Sandbox előfizetés esetén

Ez esetben [ezt a Sandbox előfizetést](https://learn.microsoft.com/en-us/training/modules/host-a-web-app-with-azure-app-service/3-exercise-create-a-web-app-in-the-azure-portal?pivots=csharp) használjuk. Amint a visszaszámlálás megjelent, nyissuk meg az [Azure portált](https://portal.azure.com) külön böngészőfülön. A portálon [állítsuk be a megfelelő tenant-ot](https://docs.microsoft.com/en-us/azure/azure-portal/set-preferences#switch-and-manage-directories): _Microsoft Learn_, és érdemes a portál nyelvét is angolra [állítani](https://docs.microsoft.com/en-us/azure/azure-portal/set-preferences#language--region), a leírás az angol nyelvű felületet követi.

A Sandbox előfizetés [korlátai](https://learn.microsoft.com/en-us/training/support/faq?pivots=sandbox) miatt 
- minden erőforrást csak az előre létrehozott _learn-_ kezdetű erőforráscsoprtba hozhatunk létre. Saját erőforráscsoportot nem hozhatunk létre!
- bizonyos erőforrásokat csak a _Central US_ vagy _East US_ régióban lehet létrehozni. Minden erőforrást lehetőleg tegyünk ugyanabba a régióba!

#### Hallgatói előfizetés esetén

[Aktiválható](https://azure.microsoft.com/en-us/free/students/) egyetemi email címmel (@edu.bme.hu). A BME tenantban jön létre, így általában nem kell [tenantot váltani](https://docs.microsoft.com/en-us/azure/azure-portal/set-preferences#switch-and-manage-directories).

Minden erőforrást tegyünk ugyanabba a régióba, például nyugat-európaiba (_West Europe_).

[Hozzunk létre](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/manage-resource-groups-portal#create-resource-groups) egy üres erőforráscsoportot a labor számára, például AcmeShop néven. Ezt használjuk mindenhol, ahol erőforráscsoportot kell megadnunk.

#### Kivonat

| Előfizetés       | Sandbox                     | Hallgatói                    |
|------------------|-----------------------------|------------------------------|
| Tenant           | Microsoft Learn             | BME                          |
| Erőforráscsoport | learn-* <br>(kötött)        | AcmeShop<br>(választható)    |
| Régió            | ált. Central US<br>(némileg kötött) | West Europe<br>(választható) |

### Előkészítés - Azure CLI

Csatlakozzunk az Azure fiókunkhoz terminálból:

```powershell
az login
```

Ha több előfizetés van a fiókunk alatt, akkor érdemes explicit kijelölni az Azure CLI által használatos alapértelmezett előfizetést. A használni kívánt előfizetés azonosítóját kell megadnunk, amit az [Azure portálról](https://learn.microsoft.com/en-us/azure/azure-portal/get-subscription-tenant-id#find-your-azure-subscription) is másolhatunk.

```powershell
az account set -s subscriptionid
```

### Előkészítés - Telepítendő alkalmazás

Használjuk az MVC-s labor megoldását (gyakorlatvezető biztosítja)!

A solution könyvtárában állva adjuk ki a parancssorban.

```powershell
dotnet build
```

Ellenőrizzük, hogy lefordul-e.

### Feladat 1 - Azure SQL adatbázis létrehozása

Navigáljunk ide: Azure portál [SQL szolgáltatásválasztó](https://portal.azure.com/#create/Microsoft.AzureSQL), itt Azure SQL Database kell.

**Adatbázis neve:** AcmeShop

**Szerver neve:** _<neptun kód>dbsrv_ (egyedinek kell lennie Azure-ban!)

**Authentikáció:** SQL

**Admin felhasználó neve:** acmeadmin

**Admin felhasználó jelszava:** ami megfelel a szabályoknak, pl. XXDBAdm1n456.

**Elastic pool:** nem

**Workload:** Development

**Compute + storage:** ha a fentit Development-re állítottuk, akkor itt már eleve a jó értéknek (General Purpose - Serverless) kell lennie.

**Backup storage redundancy:** locally-redundant

#### Network lap

**Connectivity method:** Public endpoint

**Allow Azure services and resources to access this server:** igen

**Add current client IP address:** igen

A többi lapon nem kell átállítani semmit, pl. **ne** töltessük fel adattal előre az adatbázist (*Use existing data*)

[Képes segédlet](https://learn.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart?view=azuresql&tabs=azure-portal#create-a-single-database)

### Feladat 2 - Azure App Service létrehozás

Navigáljunk ide: Azure portál [App Service űrlap](https://portal.azure.com/#create/Microsoft.WebSite)

**App neve:** _<neptun kód>acmeshop_ (egyedinek kell lennie Azure-ban!)

**Publish:** Code

**Runtime stack:** .NET 8

**OS:** Linux

**Linux plan neve:** acmeplan (_Create new_ linkre kattintva lehet megadni)

**Plan:** Free F1

Egy előfizetésben csak korlátozott számú F1 plan lehet, de ez a Sandbox előfizetés esetén általában nem probléma.

#### Monitoring lap

**Enable Application Insights:** nem

A többi lapon nem kell átállítani semmit.

### Feladat 3 - Adatbázis kapcsolódás ellenőrzés

Kapcsolódjunk az Azure portálba beépített [kezelő eszközzel](https://learn.microsoft.com/en-us/azure/azure-sql/database/connect-query-portal?view=azuresql#connect-to-the-query-editor). Lekérdezést még nem tudunk írni, mert üres az adatbázis.

Ezzel ellenőrizzük, hogy

- jól tudjuk-e kapcsolódási adatokat (felhasználónév, jelszó)
- a gépünk IP-je engedélyezett-e az adatbázis **szerver** tűzfalán

A jelszót kivéve mindent megtudhatunk [a portálról](https://learn.microsoft.com/en-us/azure/azure-sql/database/connect-query-content-reference-guide?view=azuresql#get-server-connection-information).

A jelszót nem lehet lekérdezni, [de felül lehet írni](https://learn.microsoft.com/en-us/azure/azure-sql/database/logins-create-manage?view=azuresql#existing-logins-and-user-accounts-after-creating-a-new-database).

Utólag is [hozzáadhatunk tűzfalszabályt](https://learn.microsoft.com/en-us/azure/azure-sql/database/firewall-create-server-level-portal-quickstart?view=azuresql#create-a-server-level-ip-based-firewall-rule) az SQL Server erőforráshoz.

### Feladat 4 - Adatbázistartalom inicializálása

A webes projekt könyvtárában állva inicializáljuk az adatbázist az EF Core migrációs eszközzel. A macskaköröm között a valós connection string-et adjuk meg. Egy nem valós jelszót tartalmazó connection string az [Azure portálról beszerezhető](https://learn.microsoft.com/en-us/azure/azure-sql/database/connect-query-content-reference-guide?view=azuresql#get-adonet-connection-information-optional---sql-database-only). Figyeljünk rá, hogy a valós jelszó kerüljön végül a parancsba:

```powershell
dotnet ef database update --connection "connection string"
```
Ha esetleg nem találja a parancsot:

```powershell
dotnet tool install --global dotnet-ef --version 8.*
```

### Feladat 5 - App Service konfigurálása

Az appsettings.json-ból nézzük meg, hogy az app milyen nevű connection string-et vár (pl. AcmeShopContext). Ezzel a névvel [vegyünk fel egy SQLAzure típusú connection string-et az App Service konfigurációba](https://learn.microsoft.com/en-us/azure/app-service/configure-common?tabs=portal#configure-connection-strings) (Environment variables menüpont).

A connection string értéke legyen ugyanaz, mint az adatbázistartalom inicializálásakor, de ne legyen benne/körülötte macskaköröm.

Ne felejtsünk menteni a konfigurációs oldalon!

### Feladat 6 - Alkalmazás telepítése

Generáltassuk a projekt linuxra telepíthető verzióját. A webes projekt mappájában:

```powershell
dotnet publish -r linux-x64 --no-self-contained -o ..\publish
```

A webes projekt mappájával egy szinten jön létre egy _publish_ nevű mappa. Tömörítsük le a mappa tartalmát úgy, hogy a mappa maga ne kerüljön bele a ZIP fájlba, csak a benne lévő fájlok (a _publish_ mappába menjünk be, mindent jelöljünk ki és tömörítsük).

<!-- A webes projekt mappájával egy szinten jön létre egy _publish_ nevű mappa. Tömörítsük, de maga a mappa ne kerüljön a zip-be, csak a tartalma. A _publish_ mappában állva:

```powershell
Compress-Archive .\* publish.zip
``` -->

Jöhet a telepítőcsomag csomag feltöltése az App Service-be. A resource group (_resource-group_ paraméter) és az App Service nevét (_name_) cseréljük le a saját környezetünknek megfelelően. A _publish_ mappában állva:

```powershell
az webapp deploy --resource-group acmegroup --name acmeshop --src-path publish.zip --type zip
```

### Feladat 7 - Kipróbálás

Próbáljunk ki egy adatbázis módosítással és/vagy beszúrással járó műveletet. A művelet eredményét ellenőrizzük a kapcsolódó listázó felületen.

### Erőforrások felszabadítása

Ha sandbox előfizetést használunk, akkor nem szükséges, egyébként [töröljük az erőforráscsoportot](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/delete-resource-group?tabs=azure-portal#delete-resource-group).

### Epilógus

Biztonsági szempontból nem teljesen modern a megoldásunk, de a gyakorlat keretében sokszor kénytelenek voltunk az egyszerűbb megoldást választani.
- az ajánlások szerint az adatbázis elérést már **hálózati szinten** kizárólag az App Service-re (pontosabban az App Service és az adatbázisszerver közös virtuális hálózatára) korlátozzák. Ez körülményesebbé teszi az adatbázisséma létrehozását, módosítását. Egy ilyen megoldásra [példa](https://learn.microsoft.com/en-us/azure/app-service/tutorial-dotnetcore-sqldb-app).
- érdemes az adatbázisban az SQL alapú azonosítás helyett az Azure AD alapút választani, ezzel teljesen kiváltható a connection string jelszó és ennek teljes kezelése.
- érdemes az alkalmazásnak (App Service-nek) az adminisztrátor(ok)tól teljesen külön (Azure AD-s) adatbázisfelhasználót létrehozni és ennek a felhasználónak csak annyi adatbázisos jogot adni, amire az alkalmazásnak feltétlenül szüksége van. Az alkalmazás kizárólag ezen felhasználóval kapcsolódjon, míg az adminisztrátorok az adminisztrátori felhasználóval elvégzik a séma módosítását, amikor szükséges. Egy ilyen megoldásra [példa](https://learn.microsoft.com/en-us/azure/app-service/tutorial-connect-msi-sql-database?tabs=windowsclient%2Cefcore%2Cdotnet).

## Önálló rész
Az alábbi online tananyagot kell elvégezni az edu.bme.hu-s fiókotokkal belépve.
[Magyar](https://learn.microsoft.com/hu-hu/training/modules/develop-azure-sql-database/), [Angol](https://learn.microsoft.com/en-us/training/modules/develop-azure-sql-database/)

Az első leckék részletes leírást adnak az Azure SQL adatbázissal kapcsolatos képességeiről és sajátosságairól. Amelyik lecke/unit címe **nem** azzal kezdődik, hogy Gyakorlat/Exercise, azt nem kell végrehajtani, csak el kell olvasni - hiába szólít fel erre a szöveg. A jegyzőkönyvbe a közös rész feladatain kívül az önálló részben készített erőforrásokról képernyőkép, a sikeres GitHub Action futásról illetve a végén egy adatbázisba beszúrt Employeet (saját név és neptun kód értékekkel).

A Sandbox előfizetés nem támogatja a Service Principal létrehozását, így a GitHub Action használata nem lehetséges vele. Használj inkább Azure for Students előfizetést. 

Néhány segítség és javítás az anyaghoz:
- `SQL Database Projects` parancs akkor fut csak, ha fent van az *SQL Database Projects* extension (Ctrl+Shift+X és telepítsd fel, ha nincs)
- Ha a _Push_ nem elérhető, akkor _Publish_ gombra kell kattintani és utána látod a GitHub oldalán a változtatásokat (ekkor szinkronizálod a brancheket)
- Ha az az `ad sp create-for-rbac` parancsra *Insufficient privileges to complete the operation.* választ ad, akkor Sandbox módban vagyunk és nem tudjuk kiadni a parancsot.
- a portálon több ADO.NET-es connection string is található, azt használjuk, amiben ki van hagyva a hely a jelszónak (`Password={your_password};`) 
- nem mindig van szabad erőforrás a CentralUS régióban, így hiba esetén (quota error) érdemes más régióval próbálkozni, pl. East Us
- A GitHub Action számára elkészítendő json változó kinyeréséhez az `--sdk-auth` kulcsszó kiadása is kell, a teljes parancs:
```vim
az ad sp create-for-rbac --name "MyDBProj" --role contributor --scopes /subscriptions/<your-subscription-id> --sdk-auth
```
- Erre kapott json válasz formája (ezt kell bemásolni a secretbe):
```
{
  "clientId": "...",
  "clientSecret": "...",
  "subscriptionId": "...",
  "tenantId": "...",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com",
  "resourceManagerEndpointUrl": "https://management.azure.com/",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com/",
  "managementEndpointUrl": "https://management.core.windows.net/"
}
```

- A leckében szereplő `main.yml` hibára fut, használd a következőt:
```yml
 name: Build and Deploy SQL Database Project
 on:
   push:
     branches:
       - main
 jobs:
   build:
     permissions:
       contents: 'read'
       id-token: 'write'
          
     runs-on: ubuntu-latest  # Can also use windows-latest depending on your environment
     steps:
       - name: Checkout repository
         uses: actions/checkout@v3

     # Install the SQLpackage tool
       - name: sqlpack install
         run: dotnet tool install -g microsoft.sqlpackage
    
       - name: Login to Azure
         uses: azure/login@v1
         with:
           creds: ${{ secrets.AZURE_CREDENTIALS }}
           auth-type: SERVICE_PRINCIPAL
    
       # Build and Deploy SQL Project
       - name: Build and Deploy SQL Project
         uses: azure/sql-action@v2.3
         with:
           connection-string: ${{ secrets.AZURE_CONN_STRING }}
           path: './MyDBProj/MyDBProj.sqlproj'
           action: 'publish'
           build-arguments: '-c Release'
           arguments: '/p:DropObjectsNotInSource=true'
```
- A végén ne felejtsd el kitörölni az előfizetés resource-ait, különben az előfizetésed fogja terhelni.


Amennyiben megtetszett a tananyag, a [learning path](https://learn.microsoft.com/en-us/training/paths/develop-data-driven-app-sql-db/) többi modulját is csináld meg nyugodtan.




Az itt található oktatási segédanyagok a BMEVIAUBB04 tárgy hallgatóinak készültek. Az anyagok oly módú felhasználása, amely a tárgy oktatásához nem szorosan kapcsolódik, csak a szerző(k) és a forrás megjelölésével történhet.

Az anyagok a tárgy keretében oktatott kontextusban értelmezhetőek. Az anyagokért egyéb felhasználás esetén a szerző(k) felelősséget nem vállalnak.
