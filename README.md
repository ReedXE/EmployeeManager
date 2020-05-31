# EmployeeManager
Projekt na programowanie komponentowe - PK S4 2020.
# Wymagania
```
VisualStudio z obsługą ASP.Net Core, Baza danych.
```
# Instalacja
Do poprawnego działania aplikacji wymagane jest usunięcie folderu:
```
Migrations
```
Następnym krokiem jest uruchomienie konsoli menedżera pakietów NuGet, gdy już mamy otwartą konsole wpisujemy następujące komendy
```
Add-Migration nazwa_migracji -Context EmployeeContext
```
oraz
```
Update-Database
```
Po wykonaniu obu tych komend powinna się utworzyć nowa baza danych.
Ścieżka do bazy znajduje się w pliku appsettings.json
```
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NAZWA_BAZY;Trusted_Connection=True;MultipleActiveResultSets=true"
```
W przypadku używania aplikacji lokalnie, wymagane jest dodanie użytkowników do ról w bazie danych.
W tabeli:
```
AspNetRole
```
Dodajemy role:
```
EmployeeAdministrators
EmployeeManagers
```
Następnie przypisujemy role do użytkownika w tabeli:
```
AspNetUserRoles
```
Poprzez podanie Id roli oraz Id użytkownika.

# Role
Użytkownicy, którzy rejestrują się na stronie zostają dodanie do domyślnej grupy, która może wyświetlić tabele z pracownikami.
```
EmployeeAdministrators
```
Role pozwalająca w pełni edytować baze danych (operacje CRUD).
```
EmployeeManagers
```
Rola pozwalająca dodawać nowych pracowników oraz edytować ich zawartość (np. czy są zatrudnieni, czy też nie)
