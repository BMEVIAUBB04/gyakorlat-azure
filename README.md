# Azure alapú webhoszting

## Célkitűzés
Adatbázist használó webalkalmazás telepítése Azure környezetbe. Azure menedzsment eszközök megismerése, pl. Azure portál, Azure Cloud Shell. Azure erőforrások létrehozása és konfigurálása. ADO.NET alapú adatelérés (az önálló részben).


## Előfeltételek

A labor elvégzéséhez szükséges eszközök:

- Visual Studio 2019 
  - .NET 5 SDK-val és
  - _Azure Development_ és _Data storage and processing_ workloadokkal telepítve

Amit érdemes átnézned:

- Azure előadások anyaga

## Gyakorlat menete

A közös rész és az önálló rész gyakorlatilag független, bármilyen sorrendben elvégezhető.

## Közös rész
A videó alapján (9. labor - Azure webhoszting gyakorlat). A videó tavalyi, azóta történt pár változás.

Azure SQL létrehozás:

- A biztonsági szolgáltatást kiemelték egy új _Security_ nevű fülre (_Azure Defender for SQL_) az _Additional Settings_ fülről - nem kell bekapcsolni
	
App Service létrehozás:

- Runtime stack _.NET 5_ legyen
- Megjelent egy új fül (Deployment) - ne változtassunk ott semmin
- A _Monitoring_ fülön a .NET 5 miatt a monitorozást (_Application Insights_) nem lehet bekapcsolni - ez jogos, még nem támogatott, kézzel kellene beállítani, de ezzel nem foglalkozunk

Azure SQL tűzfalszabályok:

- Tipp: a saját IP-t sokkal könnyebb hozzáadni a felület tetején az _Add Client IP_ gombbal. Menteni ne felejtsetek el utána.

Publikálás Visual Studio-ból:

- A felület kinézetre [átalakult](https://docs.microsoft.com/en-us/visualstudio/deployment/quickstart-deploy-to-azure?view=vs-2019#publish-to-azure-app-service-on-windows), de ugyanazokat az adatokat kell megadni. Az utolsó lépésben kell megadni, hogy _publish profile_ (_pubxml_) fájlt hozzon létre.

App Service extra szolgáltatások:

- Az _Application Insights_ menüpont a fentebb említettek miatt ki lesz szürkítve


[Sandbox](https://docs.microsoft.com/hu-hu/learn/modules/develop-app-that-queries-azure-sql/3-exercise-create-tables-bulk-import-query-data)

### Alap Main függvény

```csharp
public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
```

### Csökkentett jogú felhasználó létrehozása
```tsql
-- Master-en kell futtatni
CREATE LOGIN acmeuser WITH password='ACMEdb123.';
-- NEM! a master-en kell futtatni
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

Amelyik lecke/unit címe **nem** azzal kezdődik, hogy Gyakorlat/Exercise, azt nem kell végrehajtani, csak el kell olvasni - hiába szólít fel erre a szöveg.

---

Az itt található oktatási segédanyagok a BMEVIAUBB04 tárgy hallgatóinak készültek. Az anyagok oly módú felhasználása, amely a tárgy oktatásához nem szorosan kapcsolódik, csak a szerző(k) és a forrás megjelölésével történhet.

Az anyagok a tárgy keretében oktatott kontextusban értelmezhetőek. Az anyagokért egyéb felhasználás esetén a szerző(k) felelősséget nem vállalnak.
