using System.Linq;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Base;

namespace Domain.Systems {
    public class ObjectsUpdateSystem : SystemBase, IUpdateSystem {
        private ObjectsState State { get; }

        private bool active;

        public ObjectsUpdateSystem(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            State = state.objects;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
        }

        private void Reset() {
            active = false;
        }


        public void Upd(float deltaTime) {
            if (!active) return;

            // Update Player
            State.player.Upd(deltaTime);

            // Update Enemies
            foreach (Ufo ufo in State.ufosPool.Active) ufo.Upd(deltaTime);
            foreach (Asteroid asteroid in State.asteroidPools.Values.SelectMany(asteroidsPool => asteroidsPool.Active)) {
                asteroid.Upd(deltaTime);
            }

            // Update Ammo (use reverse loop - because the list is subject to change)
            foreach (Bullet bullet in State.ammo1Pool.Active) bullet.Upd(deltaTime);
            foreach (Laser laser in State.ammo2Pool.Active) laser.Upd(deltaTime);
        }
    }
}