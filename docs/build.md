# Build

Restore the Functions project before building the solution, and again after a clean (otherwise `AZFW0108`). A rebuild (`--no-incremental`) cleans that restore, so it also reports `AZFW0108`.

```
dotnet restore src/Template.Functions/Template.Functions.csproj
dotnet build Template.Functions.slnx
```
