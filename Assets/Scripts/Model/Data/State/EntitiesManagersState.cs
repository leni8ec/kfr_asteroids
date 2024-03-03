using System.Collections.Generic;
using Model.Data.EntityPool;
using Model.Data.State.Base;
using Model.Data.Unity.Config;
using Model.Entity;

namespace Model.Data.State {
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