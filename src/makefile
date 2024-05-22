cat:
	dotnet run --project Services/Catalog/Catalog.API

buildcat: 
	docker build -t catalog-api -f Services/Catalog/Catalog.API/Dockerfile .

runcat: 
	docker run catalog-api --name catalog -p 8080:8080 -p 6061:8081 -e ASPNETCORE_ENVIRONMENT=Development
	
api:
	dotnet run --project API

sapp:
	dotnet run --project WebApp --launch-profile https

sapi:
	dotnet run --project API --launch-profile https
