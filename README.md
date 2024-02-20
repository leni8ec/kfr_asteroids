## Asteroids - video game
> _Portfolio project_

![image](https://github.com/leni8ec/game-asteroids/assets/2379473/86ce4f7b-5b7e-4f0f-bf61-778990c0321d)


---

### ✅ Architecture used (design patterns)
- **MVC** - base architecture pattern (also to be able to Client/Server case?)
- **IoC, DI**  (`DependencyContainer`)
- **Factory Method** - create new entities (use in Pool)
- **Object Pool** - spawn and reuse entities
- **Command pattern** - input
- **Adapter pattern** - view implementations for some model objects (`camera`, `audio`)
- The **Data** (`state`, `config`) is split from **Logic** (`systems`)

### 🔲 Unity
- **Scriptable objects** (for config)
- **Input system**

---

### ➡️ Todo
- Code cleanup (and some documentation)
- Event System (purpose?)
