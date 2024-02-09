using Control.Model;
using Model.Core.Objects;
using Model.Core.Unity.Data;
using UnityView.Objects;

namespace UnityView.Handlers {
    public class UnityEntitiesCreateHandler {

        public UnityEntitiesCreateHandler(PrefabsCollector prefabs) {
            EntitiesCreateHandler entitiesCreateHandler = new();

            entitiesCreateHandler.AddListener<Player>(new UnityEntityCreateListener<PlayerView>(prefabs.player));
            entitiesCreateHandler.AddListener<Bullet>(new UnityEntityCreateListener<BulletView>(prefabs.bullet));
            entitiesCreateHandler.AddListener<Laser>(new UnityEntityCreateListener<LaserView>(prefabs.laser));
            entitiesCreateHandler.AddListener<Ufo>(new UnityEntityCreateListener<UfoView>(prefabs.ufo));
            // Asteroids has own logic
            entitiesCreateHandler.AddListener<Asteroid>(new UnityAsteroidCreateListener(prefabs.asteroidLarge, prefabs.asteroidMedium, prefabs.asteroidSmall));

        }

    }
}