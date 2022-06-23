docker/up:
	docker-compose up -d	

docker/down:   
	docker-compose down
 	
docker/purge:   
	docker-compose down --rmi all

db/restart: docker/down docker/up

migrate/add:
	dotnet ef migrations add $(name) --project OrderEntry.Database	

migrate/clean:
	rm -r ./OrderEntry.Database/Migrations

migrate/update:
	dotnet ef database update --project OrderEntry

migrate/init:
	make migrate/add name="Init"

migrate/refresh: db/restart migrate/clean migrate/init migrate/update 