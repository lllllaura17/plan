# plan
Планирование активностей представляет собой инструмент для оценки эффективности, безопасности и целесообразности применения различных способов передвижения (пешего, колесного, специального).

Activity Planner API

Назначение:
API управляет списком активностей и оценивает их пригодность в зависимости от погодных условий, получаемых извне (через сервис-координатор или клиент).
Модель activity:
{
  "name": "Running",
  "minTemperature": 5,
  "maxTemperature": 25,
  "allowRain": false,
  "allowLightRain": false,
  "maxWindSpeed": 10
}
Модель weatherdata:
{
  "temperature": 20,
  "isRaining": false,
  "windSpeed": 5
}
suitability:
| Значение | Описание    |
| ------—- | ---———----- |
| 0        | Подходит    |
| 1        | Допустимо   |
| 2        | Не подходит |

ЭНДПОИНТЫ:

Получить все активности:
GET /api/activities

Добавить активность:
POST /api/activities

Обновить активность:
PUT /api/activities/{name}

Удалить активность:
DELETE /api/activities/{name}

Оценить активность по погодным условиям:
POST /api/activities/evaluate
Тело запроса: модель weatherdata
