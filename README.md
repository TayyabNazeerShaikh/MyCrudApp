![alt text](https://github.com/TayyabNazeerShaikh/MyCrudApp/blob/master/Doc/a.png)
![alt text](https://github.com/TayyabNazeerShaikh/MyCrudApp/blob/master/Doc/b.png)
![alt text](https://github.com/TayyabNazeerShaikh/MyCrudApp/blob/master/Doc/c.png)
![alt text](https://github.com/TayyabNazeerShaikh/MyCrudApp/blob/master/Doc/d.png)

1. `dotnet new sln -n MyCrudApp -o MyCrudApp`

2. `cd .\MyCrudApp\`

3. `dotnet new gitignore`

4. `git init`

5. `git add .`

6. `git commit -m "Initial commit"`

7. `dotnet new mvc -n MyCrudApp.MvcApp --use-program-main`

8. `dotnet sln MyCrudApp.sln add .\MyCrudApp.MvcApp\MyCrudApp.MvcApp.csproj`

9. `dotnet build`

10. `cd .\MyCrudApp.MvcApp\`

11. `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`

12. `dotnet add package Microsoft.EntityFrameworkCore.Relational`

13. `dotnet add package Microsoft.EntityFrameworkCore.Design`

14. `dotnet add package Microsoft.EntityFrameworkCore.Tools`

15. `dotnet add package Microsoft.EntityFrameworkCore`

16. `dotnet tool install --global dotnet-ef`

17. `dotnet ef migrations add InitialCreate`

18. `dotnet ef database update`

19. `dotnet run`
