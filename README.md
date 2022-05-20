# movies-api

## How to run?

1-) It's a code first project, so you just need to update the DBConnectionString in appsettings.json with your MSSQL servers. Make sure the DBUser has the required privileges to create a database.

2-) When you run the project, all tables are automatically created and a seed data set inserted into the database. The seed data has been created randomly, it doesn't contain real data. 

3-) You can see the endpoint definitions on Swagger. Please do not forget that some endpoints use response cache. You may not the latest data until the cache is purged.

## What more can be done? 

1-) More unit tests can be added.
2-) More manuel test could be run. 
3-) Hard coded text can be moved a static constants class.
4-) Some load tests could be run. 
5-) Repositories could be created rather than using the DBCOntext directly in Queries/Commands. 


## Timeline
In total, I spent about 10 hours partially completing the test.
