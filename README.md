# Azure alapú webhoszting

## Célkitűzés
Adatbázist használó webalkalmazás telepítése Azure környezetbe. Azure menedzsment eszközök megismerése, pl. Azure portál, Azure Cloud Shell. Azure erőforrások létrehozása és konfigurálása. ADO.NET alapú adatelérés (az önálló részben).


## Előfeltételek

A labor elvégzéséhez szükséges eszközök:

- Visual Studio 2022 
  - .NET 6 SDK-val és
  - _Azure Development_ és _Data storage and processing_ workloadokkal telepítve
- SQL Server Management Studio (SSMS)

Amit érdemes átnézned:

- Azure előadások anyaga

## Gyakorlat menete

A közös rész és az önálló rész gyakorlatilag független, bármilyen sorrendben elvégezhető.

## Közös rész

[Ezen útmutató alapján](https://docs.microsoft.com/en-us/azure/app-service/tutorial-dotnetcore-sqldb-app?tabs=azure-portal%2Cvisualstudio-deploy%2Cdeploy-instructions-azure-portal%2Cazure-portal-logs%2Cazure-portal-resources). Lentebb a feladatok számozása az útmutatót követi. Amikor lehet, Azure portálon dolgozzunk.

### Előkészítés - Előfizetés

Igazi Azure előfizetés helyett [ezt a Sandbox előfizetést](https://docs.microsoft.com/hu-hu/learn/modules/develop-app-that-queries-azure-sql/3-exercise-create-tables-bulk-import-query-data) használjuk. Amint a visszaszámlálás megjelent, az [Azure portálra](https://portal.azure.com) el lehet navigálni. A portálon [állítsuk be a megfelelő tenant-ot](https://docs.microsoft.com/en-us/azure/azure-portal/set-preferences#switch-and-manage-directories): _Microsoft Learn_ és érdemes a portál nyelvét is angolra [állítani](https://docs.microsoft.com/en-us/azure/azure-portal/set-preferences#language--region).

A Sandbox előfizetés korlátai miatt 
- minden erőforrást csak az előre létrehozott _learn-_ kezdetű erőforráscsoprtba hozhatunk létre. Saját erőforráscsoportot nem hozhatunk létre!
- bizonyos erőforrásokat csak a _Central US_ régióban lehet létrehozni. Minden erőforrást tegyünk ebbe a régióba!

### Feladat 1 - Mintaalkalmazás

Használjuk az MVC-s labor megoldását!

### Feladat 2 - Azure App Service létrehozás

- az útmutató szerint, figyelembe véve a Sandbox előfizetés miatti korlátokat.
- az app nevének egyedinek kell lennie, érdemes a neptunkódot valahogy beletenni, pl. _<neptun kód>shop_

### Feladat 3 - Adatbázis létrehozás, felhasználó létrehozása

- az útmutató szerint, figyelembe véve a Sandbox előfizetés miatti korlátokat.
- a szerver nevének egyedinek kell lennie, érdemes a neptunkódot valahogy beletenni, pl. _<neptun kód>srv_
- az adatbázis (nem a szerver) nevének nem kell egyedinek lennie, legyen: _AcmeShop_
- az adatbázis (nem a szerver) létrehozásakor a _Networking_ fülön már eleve hozzá lehet adni a gépünk IP-jét

#### Extra lépések

1. Ha nem adtuk hozzá a gépünk IP-jét a szerver tűzfalszabályaihoz, mint engedélyezett IP címet, akkor tegyük meg most. A 6-os feladat leírása ismerteti ennek mikéntjét.
1. Csatlakozzunk a létrehozott adatbázisból SSMS-ből. A csatlakozáshoz az adatok (kivéve a jelszót) az adatbázis _Connection Strings_ lapján kiolvashatóak.
1. Csökkentett jogú felhasználó létrehozása
	```tsql
	-- Master adatbázison kell futtatni
	CREATE LOGIN acmeuser WITH password='ACMEdb123.';
	-- NEM! a master-en kell futtatni
	CREATE USER acmedbuser FROM LOGIN acmeuser;
	EXEC sp_addrolemember 'db_datareader', 'acmedbuser';
	EXEC sp_addrolemember 'db_datawriter', 'acmedbuser';
	
	-- csak ellenorzeshez
	select name as username,
	       create_date,
	       modify_date,
	       type_desc as type,
	       authentication_type_desc as authentication_type
	from sys.database_principals
	where type not in ('A', 'G', 'R', 'X')
	      and sid is not null
	order by username;
	```

### Feladat 4 - Alkalmazás telepítése

Később, most hagyjuk ki.

### Feladat 5 - App Service konfigurálása

- az útmutató szerint, de a felhasználónév-jelszó _acmeuser-ACMEdb123._ legyen.
- a keresőmezőbe értelemszerűen ne az ott megadott neveket, hanem a korábban megadott adatbázis és app neveket írjuk

### Feladat 6 - Adatbázistartalom inicializálása

- Az első lépéseket (Azure SQL tűzfal konfigurálása) már végrehajtottuk korábban
- Ne az appsettings.json-ba tegyük a connection string-et, hanem a [secrets.json fájlba](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux#enable-secret-storage)
- A connection string neve _AcmeShopContext_ legyen

Extra lépésként **most hajtsuk végre** a 4-es feladatot. Visual Studio-ból telepítsünk és az MVC-s gyakorlat megoldását, végállapotát.

### Feladat 7 - Alkalmazás kipróbálása

- Próbáljuk ki a telepített alkalmazást például egy termék módosításával. Ellenőrizzük SSMS-ből is, hogy az Azure-os adatbázisban megtörtént-e a módosítás.

### Feladat 8 - Naplózás bekapcsolása

- Opcionális, idő spórolás miatt csak akkor érdemes bekapcsolni, ha a telepített alkalmazásban hibát tapasztalunk.

### Erőforrások felszabadítása

- Sandbox előfizetést használunk, így nem szükséges.


## Önálló rész
Az alábbi online tananyagot kell elvégezni az edu.bme.hu-s fiókotokkal belépve.
[Magyar](https://docs.microsoft.com/hu-hu/learn/modules/develop-app-that-queries-azure-sql/) [Angol](https://docs.microsoft.com/en-us/learn/modules/develop-app-that-queries-azure-sql/)

Amelyik lecke/unit címe **nem** azzal kezdődik, hogy Gyakorlat/Exercise, azt nem kell végrehajtani, csak el kell olvasni - hiába szólít fel erre a szöveg.

---

Az itt található oktatási segédanyagok a BMEVIAUBB04 tárgy hallgatóinak készültek. Az anyagok oly módú felhasználása, amely a tárgy oktatásához nem szorosan kapcsolódik, csak a szerző(k) és a forrás megjelölésével történhet.

Az anyagok a tárgy keretében oktatott kontextusban értelmezhetőek. Az anyagokért egyéb felhasználás esetén a szerző(k) felelősséget nem vállalnak.
