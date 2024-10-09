dotnet new sln -n MyCrudApp -o MyCrudApp

cd .\MyCrudApp\

dotnet new gitignore

git init

git add .

git commit -m "Initial commit"

dotnet new mvc -n MyCrudApp.MvcApp --use-program-main

dotnet sln MyCrudApp.sln add .\MyCrudApp.MvcApp\MyCrudApp.MvcApp.csproj

dotnet build

cd .\MyCrudApp.MvcApp\

dotnet add package Microsoft.EntityFrameworkCore.Sqlite   

dotnet add package Microsoft.EntityFrameworkCore.Relational

dotnet add package Microsoft.EntityFrameworkCore.Design 

dotnet add package Microsoft.EntityFrameworkCore.Tools     

dotnet add package Microsoft.EntityFrameworkCore 

dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate

dotnet ef database update

dotnet run
