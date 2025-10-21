# 🗼 SimpleTD - Tower Defense Game

> Демонстрационный проект Tower Defense с Clean Architecture и современным стеком технологий

## 📋 Описание

Tower Defense игра, демонстрирующая применение чистой архитектуры и современных паттернов в Unity. Игрок защищает линию от волн зомби, размещая турели на поле. Покупка турелей осуществляется за монеты, которые генерируются пассивно и за убийство врагов.

**Геймплей:** Выживи N минут, отбивая волны зомби с помощью стратегического размещения турелей.

## 🛠 Технологии

- **Unity** 2022.3.62f1
- **Zenject** - Dependency Injection контейнер
- **C#** 10.0+

## 🏗 Архитектура

### Clean Architecture + State Machine

Проект построен на принципах чистой архитектуры с глобальной State Machine для управления состояниями игры.

```
├── Infrastructure/          # Инициализация и DI
│   ├── Bootstrap           # Точка входа
│   ├── GlobalStateMachine  # FSM для game flow
│   └── DI/                 # Zenject installers
├── StateMachine/           # Состояния игры
│   └── States/             # Bootstrap, Menu, Gameplay states
└── Game Logic/             # Игровая логика (отделена от инфраструктуры)
```

### Ключевые паттерны

**Global State Machine:**
- Управление переходами между состояниями (Bootstrap → Menu → Game)
- Изоляция логики каждого состояния
- Чистые переходы без циклических зависимостей

**Dependency Injection (Zenject):**
- `GlobalInstaller` - глобальные сервисы
- Привязка состояний FSM через контейнер
- Lazy initialization для оптимизации

**Bootstrap Pattern:**
- `Bootstrap.cs` - входная точка приложения
- Инициализация DI контейнера
- Переход в начальное состояние (BootstrapState)
- `DontDestroyOnLoad` для сохранения между сценами

## 📦 Основные компоненты

### Infrastructure Layer

**Bootstrap**
```csharp
public class Bootstrap : MonoBehaviour
{
    [Inject]
    public void Constructor(GlobalStateMachine machine) 
        => _stateMachine = machine;
    
    private void Start()
    {
        _stateMachine.ChangeState<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}
```
- Инициализация приложения
- Injection глобальной State Machine
- Сохранение между сценами

**GlobalStateMachine**
```csharp
public class GlobalStateMachine
{
    private readonly Dictionary<Type, IState> _stateDictionary;
    private IState _currentState;
    
    public void ChangeState<T>() where T : IState
    {
        _stateDictionary[typeof(T)].Enter();
        _currentState = _stateDictionary[typeof(T)];
    }
}
```
- Type-safe переключение состояний
- Централизованное управление game flow
- Регистрация состояний через DI

**GlobalInstaller (Zenject)**
- Регистрация `GlobalStateMachine` как Singleton
- Привязка всех состояний (Bootstrap, Menu, Gameplay)
- Настройка NonLazy для немедленной инициализации

### State Machine States

**IState Interface**
```csharp
public interface IState
{
    void Enter();
    void Exit();
}
```

**Planned States:**
- `BootstrapState` - начальная загрузка, инициализация систем
- `MenuState` - главное меню
- `GameplayState` - игровой процесс
- Возможность расширения для Pause, GameOver, Victory

## 🎯 Игровые механики (Planned)

- ✅ **Волновая система** - волны зомби с увеличивающейся сложностью
- ✅ **Экономика** - пассивный доход + награды за убийства
- ✅ **Турели** - различные типы с уникальными характеристиками
- ✅ **Размещение** - стратегическое позиционирование на линии
- ✅ **Прогрессия** - таймер выживания
- ✅ **Addressables** - динамическая загрузка турелей и врагов
- ✅ **UniTask** - асинхронная загрузка ассетов
- ✅ **DOTween** - плавные анимации турелей и UI

## 🚀 Как запустить

1. Клонировать репозиторий:
```bash
git clone https://github.com/asasin56/SimpleTD
```

2. Открыть проект в Unity 2022.3.62f1 или выше

3. Открыть сцену Bootstrap: `Assets/Scenes/Bootstrap.unity`

5. Нажать Play

## 💡 Чему научился в проекте

### Архитектурные паттерны:
- Применение Clean Architecture в Unity
- Global State Machine для game flow
- Dependency Injection для управления зависимостями
- Separation of Concerns (Infrastructure / Game Logic)

### Технические навыки:
- Работа с Zenject контейнером
- Асинхронная загрузка через UniTask
- Управление ассетами через Addressables
- Bootstrap pattern для инициализации приложения
- Type-safe работа с состояниями

### Проектирование:
- Масштабируемая архитектура
- Слабая связанность компонентов
- Testability через DI
- Расширяемость через State Machine

## 🔧 Архитектурные решения

### Почему Global State Machine?

**Проблема:**
Типичные Unity игры используют прямые переходы между сценами через `SceneManager.LoadScene()`, что создаёт:
- Сильную связанность
- Сложность управления состоянием
- Проблемы с тестированием

**Решение:**
Глобальная State Machine с типизированными состояниями:
- ✅ Централизованное управление переходами
- ✅ Легко добавить новые состояния
- ✅ Четкая логика каждого состояния
- ✅ Тестируемость

### Почему Zenject?

- **Testability** - легко мокать зависимости
- **Decoupling** - компоненты не знают о создании друг друга
- **Lifecycle management** - контейнер управляет жизненным циклом
- **Scalability** - легко добавлять новые сервисы


## 🎓 Ключевые концепции

- **Clean Architecture** - разделение на слои (Infrastructure, Domain, Presentation)
- **Dependency Inversion** - зависимость от абстракций, не реализаций
- **Single Responsibility** - каждый класс отвечает за одну вещь
- **Open/Closed Principle** - легко расширять, не изменяя существующий код
- **State Pattern** - управление состояниями приложения
- **Bootstrap Pattern** - централизованная инициализация

## 🔍 Детали реализации

### Bootstrap Flow

```
Application Start
    ↓
Bootstrap.Start()
    ↓
Zenject Injection
    ↓
GlobalStateMachine.ChangeState<BootstrapState>()
    ↓
BootstrapState.Enter()
    ↓
Initialize Core Systems
    ↓
Load Menu/Game
```

### State Transitions

```
BootstrapState
    ↓
MenuState ⟷ GameplayState
    ↓
GameOverState / VictoryState
    ↓
Back to MenuState
```

## 📝 Что можно улучшить

### Если бы это был production-проект:

- Добавить юнит-тесты для State Machine
- Реализовать Object Pooling для снарядов и врагов
- Добавить систему сохранений (прогресс, настройки)
- Реализовать аналитику и метрики
- Добавить локализацию
- Оптимизировать через ECS (DOTS) для большого кол-ва юнитов

### Текущий технический долг:

- Gameplay логика пока не реализована (в процессе)
- Нужно добавить обработку ошибок в загрузке Addressables
- State Machine может иметь History для навигации назад
- Добавить визуальные переходы между состояниями

## 🆚 Сравнение с другими проектами

### SimpleTD vs ECS Platformer (VARP)

| Аспект | VARP | SimpleTD |
|--------|------|----------|
| Архитектура | ECS (Entitas) | Clean Architecture + FSM |
| DI | Атрибуты [Inject] | Zenject Container |
| Состояния | Контексты Entitas | State Machine |
| Async | Coroutines | UniTask |
| Assets | Resources | Addressables |
| Фокус | Data-Oriented | Layered Architecture |

**Вывод:** Оба проекта показывают разные подходы к архитектуре в Unity. VARP - про ECS и производительность, SimpleTD - про чистую архитектуру и масштабируемость.

## 📞 Контакты

- GitHub: https://github.com/asasin56
- Telegram: @nikoraidesu
- Email: skorpion19887@gmail.com

---

⭐ Если проект был полезен, поставьте звезду на GitHub!
