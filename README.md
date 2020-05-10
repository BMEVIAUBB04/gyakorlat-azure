# Azure alapú webhoszting

## Célkitűzés
Adatbázist használó webalkalmazás telepítése Azure környezetbe. Azure menedzsment eszközök megismerése, pl. Azure portál, Azure Cloud Shell. Azure erőforrások létrehozása és konfigurálása.


## Előfeltételek

A labor elvégzéséhez szükséges eszközök:

- Visual Studio 2019 .NET Core 3.1 SDK-val telepítve

Amit érdemes átnézned:

- Azure előadások anyaga

## Gyakorlat menete

A közös rész és az önálló rész gyakorlatilag független, bármilyen sorrendben elvégezhető.

## Közös rész
A videó alapján (9. labor - Azure webhoszting gyakorlat).

[Sandbox](https://docs.microsoft.com/hu-hu/learn/modules/develop-app-that-queries-azure-sql/3-exercise-create-tables-bulk-import-query-data)

### Alap Main függvény

```csharp
public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
```

### Csökkentett jogú felhasználó létrehozása
```tsql
CREATE LOGIN acmeuser WITH password='ACMEdb123.';
CREATE USER acmedbuser FROM LOGIN acmeuser;
EXEC sp_addrolemember 'db_datareader', 'acmedbuser';
EXEC sp_addrolemember 'db_datawriter', 'acmedbuser';

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

## Önálló rész
Az alábbi online tananyagot kell elvégezni az edu.bme.hu-s fiókotokkal belépve.
[Magyar](https://docs.microsoft.com/hu-hu/learn/modules/develop-app-that-queries-azure-sql/) [Angol](https://docs.microsoft.com/en-us/learn/modules/develop-app-that-queries-azure-sql/)

Amelyik almodul címe **nem** azzal kezdődik, hogy Gyakorlat/Exercise, azt nem kell végrehajtani, csak el kell olvasni - hiába szólít fel erre a szöveg.

---

Az itt található oktatási segédanyagok a BMEVIAUBB04 tárgy hallgatóinak készültek. Az anyagok oly módú felhasználása, amely a tárgy oktatásához nem szorosan kapcsolódik, csak a szerző(k) és a forrás megjelölésével történhet.

Az anyagok a tárgy keretében oktatott kontextusban értelmezhetőek. Az anyagokért egyéb felhasználás esetén a szerző(k) felelősséget nem vállalnak.
