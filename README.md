## Entwicklungsumgebung erstellen

Es sollten schon .NET 10, Git, Docker Desktop und Visual Studio Code onstalliert sein

Ablauf:
1. Repository klonen und Ordner in Visual Studio Code öffnen
2. Git Hooks einschalten mit: 'git config core.hooksPath .githooks'
3. Builden mit Ctrl+Shift+B


## Projekt starten

Um ein Projekt zu starten sollten folgende Schritte ausgeführt werden

1. 3 verschiedene Terminals öffnen
2. Mit "cd" im Ordner MiniShop gehen
3. Je die untenstehenden Commands in je einen Terminal ausführen
    dotnet run --project src/MiniShop/DeliveryService
    dotnet run --project src/MiniShop/ShopMonolith
    dotnet run --project src/MiniShop/Gateway
4. http://localhost:5000 öffnen