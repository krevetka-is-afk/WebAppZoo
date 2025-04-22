# WebAppZoo
# Система управления зоопарком, реализованная с использованием принципов Clean Architecture и концепций Domain-Driven Design.
**Выполнил:** Растворов Сергей 236
## Реализованный функционал

### 1. Доменная модель (Domain Layer)
- `Animal.cs` - сущность животного с методами:
    - Feed() - кормление животного
    - Treat() - лечение животного
    - MoveToEnclosure() - перемещение между вольерами
- `Enclosure.cs` - сущность вольера с методами:
    - AddAnimal() - добавление животного
    - RemoveAnimal() - удаление животного
    - Clean() - очистка вольера
- `FeedingSchedule.cs` - сущность расписания кормления с методами:
    - UpdateTime() - обновление времени кормления
    - MarkCompleted() - отметка о выполнении кормления
- Доменные события в папке `Events/`:
    - `AnimalMovedEvent.cs` - событие перемещения животного
    - `FeedingTimeEvent.cs` - событие времени кормления

### 2. Прикладной слой (Application Layer)
- Интерфейсы репозиториев в папке `Interfaces/`:
    - `IAnimalRepository.cs`
    - `IEnclosureRepository.cs`
    - `IFeedingScheduleRepository.cs`
- Сервисы в папке `Services/`:
    - `AnimalTransferService.cs` - логика перемещения животных
    - `FeedingOrganizationService.cs` - организация кормлений
    - `ZooStatisticsService.cs` - статистика зоопарка

### 3. Слой представления (Presentation Layer)
- REST API контроллеры в папке `Controllers/`:
    - `AnimalsController.cs` - управление животными
    - `EnclosuresController.cs` - управление вольерами
    - `FeedingScheduleController.cs` - управление расписанием кормления

### 4. Инфраструктурный слой (Infrastructure Layer)
- In-memory репозитории в папке `Repositories/`:
    - `InMemoryAnimalRepository.cs`
    - `InMemoryEnclosureRepository.cs`
    - `InMemoryFeedingScheduleRepository.cs`

## Примененные концепции и принципы

### Domain-Driven Design (DDD)
1. **Агрегаты и сущности**:
    - `Animal`, `Enclosure`, `FeedingSchedule` как корневые агрегаты
    - Инкапсуляция бизнес-логики внутри сущностей

2. **Доменные события**:
    - `AnimalMovedEvent` и `FeedingTimeEvent` для отслеживания изменений состояния

3. **Value Objects**:
    - Использование `Guid` для идентификаторов
    - Инкапсуляция бизнес-правил в доменных сущностях

### Clean Architecture
1. **Разделение на слои**:
    - `Domain Layer` - чистые бизнес-модели
    - `Application Layer` - бизнес-логика и use cases
    - `Infrastructure Layer` - реализация репозиториев
    - `Presentation Layer` - REST API контроллеры

2. **Зависимости**:
    - Использование интерфейсов для абстракции

3. **Принцип инверсии зависимостей**:
    - Репозитории реализуют интерфейсы из `Application Layer`
    - Контроллеры зависят от абстракций, а не от конкретных реализаций

## Тестирование API

API можно тестировать через Swagger UI, доступный по адресу `http://localhost:5036/swagger/index.html` после запуска приложения.

### Доступные операции:
1. **Животные**:
    - `GET/api/animals` - получение списка животных
    - `POST/api/animals` - добавление нового животного
    - `GET/api/animals/{id}` - получение данных животного по id
    - `DELETE/api/animals/{id}` - удаление животного по id
    - `POST/api/animals/{id}/transfer` - перемещение животного

2. **Вольеры**:
    - `GET/api/enclosures` - получение списка вольеров
    - `POST/api/enclosures` - добавление нового вольера
    - `GET/api/enclosures/{id}` - получение данных вольера по id
    - `DELETE/api/enclosures/{id}` - удаление вольера

3. **Расписание кормления**:
    - `GET/api/feeding-schedule` - получение расписания
    - `POST/api/feeding-schedule` - добавление кормления
    - `GET/api/feeding-schedule/upcoming` - получение данных по кормлению в промежуток во времени
    - `PUT/api/feeding-schedule/{id}/mark-completed` - отметка о выполнении кормления 
