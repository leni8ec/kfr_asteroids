using System.Collections.Generic;
using Model.Core.Data.EntityPool;
using Model.Core.Data.State.Base;
using Model.Core.Data.Unity.Config;
using Model.Core.Entity;

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