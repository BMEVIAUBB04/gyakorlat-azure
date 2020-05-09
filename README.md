# Azure alapú webhoszting

## Közös rész
A videó alapján.

[Sandbox](https://docs.microsoft.com/en-us/learn/modules/develop-app-that-queries-azure-sql/3-exercise-create-tables-bulk-import-query-data)

### Alap Main függvény

```csharp
public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
```

### Csökkentett jogú felhasználó létrehozása
```sql
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

### Beadás
A MS Learn profiloldalról kell beküldeni egy képernyőmentést, amin látszik az edu.bme.hu-s profil és az elvégzést igazoló jelölés.

