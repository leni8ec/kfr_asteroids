## Asteroids - video game
> _Portfolio project_

![image](https://github.com/leni8ec/game-asteroids/assets/2379473/86ce4f7b-5b7e-4f0f-bf61-778990c0321d)


---

### ✅ Architecture used (design patterns)
- **MVP** (SC) - base architecture pattern (Supervising Controller)
- **Data** (`state`, `config`) is split from **Logic** (`systems`)
- **IoC, DI** - dependency injection 
- **Object Pool** - spawn and reuse entities
- **Factory Method** - create new entities (used with Pool)
- **Command pattern** - handle input
- **Adapter pattern** - view (unity objects) implementation for some model objects (`camera`, `audio`)


### 🔲 Unity
- **Scriptable objects** (for config)
- **Input system**

---

### ➡️ Todo
- Event System (purpose?)
- Code cleanup (and some documentation)

