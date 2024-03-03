using System.Collections.Generic;
using Model.Data.EntityPool;
using Model.Data.State.Base;
using Model.Data.Unity.Config;
using Model.Entity;

namespace Model.Data.State {
    public class ActiveEntitiesState : IStateData {

        // Player
        public Player player;

        // Weapon
        /// <summary>
        /// Bullet
        /// </summary>
        public IEntitiesList<Bullet> ammo1;
        /// <summary>
        /// Laser
        /// </summary>
        public IEntitiesList<Laser> ammo2;

        // World
        public IEntitiesList<Ufo> ufos;
        public readonly Dictionary<AsteroidConfig.Size, IEntitiesList<Asteroid>> asteroidsDict = new();


        public void Reset() {
            // empty
        }

    }
}