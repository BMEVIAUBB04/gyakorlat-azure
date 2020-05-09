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
https://docs.microsoft.com/hu-hu/learn/modules/develop-app-that-queries-azure-sql/
