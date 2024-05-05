cat:
	dotnet run --project src/Services/Catalog/Catalog.API

buildcat: 
	dotnet build --project src/Services/Catalog/Catalog.API
	
api:
	dotnet run --project API

sapp:
	dotnet run --project WebApp --launch-profile https

sapi:
	dotnet run --project API --launch-profile https
