using Model.Core.Container.Object;
using Model.Core.Data.Collectors;
using Model.Core.Unity.Data.Config;
using UnityEngine;

namespace UnityView.Data {
    public class UnityConfigCollector : MonoBehaviour, ICollectorCreator<ConfigCollector> {
        [SerializeField] private PlayerConfig player;
        [SerializeField] private BulletConfig bullet;
        [SerializeField] private LaserConfig laser;

        [Space]
        [SerializeField] private AsteroidConfig asteroidLarge;
        [SerializeField] private AsteroidConfig asteroidMedium;
        [SerializeField] private AsteroidConfig asteroidSmall;

        [Space]
        [SerializeField] private UfoConfig ufo;
        [Space]
        [SerializeField] private WorldConfig world;
        [Space]
        [SerializeField] private SoundsConfig sounds;

        public ConfigCollector CreateCollector() {
            ConfigCollector collector = new();
            collector.Add(player);
            collector.Add(bullet);
            collector.Add(laser);

            collector.Add(asteroidLarge, AsteroidConfig.Size.Large);
            collector.Add(asteroidMedium, AsteroidConfig.Size.Medium);
            collector.Add(asteroidSmall, AsteroidConfig.Size.Small);

            collector.Add(ufo);
            collector.Add(world);
            collector.Add(sounds);

            return collector;
        }
    }
}