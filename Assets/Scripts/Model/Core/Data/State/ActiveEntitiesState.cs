using System.Collections.Generic;
using Model.Core.Data.EntityPool;
using Model.Core.Data.State.Base;
using Model.Core.Data.Unity.Config;
using Model.Core.Entity;

namespace Model.Core.Data.State {
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