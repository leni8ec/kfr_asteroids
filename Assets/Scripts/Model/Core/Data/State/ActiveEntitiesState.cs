using System.Collections.Generic;
using Model.Core.Entity;
using Model.Core.Interface.State;
using Model.Core.Pool;
using Model.Core.Unity.Data.Config;

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