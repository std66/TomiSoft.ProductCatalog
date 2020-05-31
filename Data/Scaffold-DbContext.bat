@ECHO OFF
cd ..
dotnet ef dbcontext scaffold "DataSource=..\Data\product_catalog.sqlite.db" Microsoft.EntityFrameworkCore.Sqlite --startup-project TomiSoft.ProductCatalog.Server --project TomiSoft.ProductCatalog.Data.Sqlite -o "Entities" --data-annotations --context "SqliteProductCatalogDbContext" --context-dir "." --force --no-build
pause