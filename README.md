# Test Project

ASP.NET Core 8.0 проєкт, що складається з двох застосунків.

## Структура

```
test-project/
├── TestProjectAPI/   # REST Web API
└── TestProjectMVC/   # MVC web application
```

### TestProjectAPI

REST API з Swagger UI.

- Продукти (FakeStore API)
- Статті (зовнішнє API)

**URL:** `https://localhost:7114` | Swagger: `/swagger`

### TestProjectMVC

Server-rendered MVC застосунок (Bootstrap 5), що споживає API.

- Перегляд продуктів та деталей
- Перегляд статей, пошук, фільтр за тегом
- Головна сторінка та Privacy

**URL:** `https://localhost:7103`

## Запуск

```bash
# API
cd TestProjectAPI/TestProjectAPI
dotnet run

# MVC
cd TestProjectMVC/TestProjectMVC
dotnet run
```

## Технології

- ASP.NET Core 8.0
- Swagger / Swashbuckle
- HttpClient / зовнішні API
- Bootstrap 5, jQuery
