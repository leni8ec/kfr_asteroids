using System.Collections.Generic;
using Model.Core.Entity;
using Model.Core.Interface.State;
using Model.Core.Pool;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Data.State {
    public class EntitiesManagersState : IStateData {

        // Weapon
        public EntitiesManager<Bullet, BulletAmmoState, BulletConfig> ammo1;
        public EntitiesManager<Laser, LaserAmmoState, LaserConfig> ammo2;

        // World
        public EntitiesManager<Ufo, UfoState, UfoConfig> ufos;
        public readonly Dictionary<AsteroidConfig.Size, EntitiesManager<Asteroid, AsteroidState, AsteroidConfig>> asteroidsManagers = new();


        public void Reset() {
            // empty
        }

    }
}