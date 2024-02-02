using UnityEngine;

namespace Core.Config {
    [CreateAssetMenu(menuName = "Data/WorldData")]
    public class WorldConfig : ScriptableObject, IConfigData {
        [Tooltip("Max asteroids count (in any sizes)")]
        public int asteroidsLimit = 20;
        [Tooltip("Max ufos count (in any sizes)")]
        public int ufosLimit = 3;

        [Space]
        [Tooltip("Count in sec")]
        public float asteroidsSpawnRate = 3;
        [Tooltip("Count in sec")]
        public float ufoSpawnRate = 0.1f;

        [Space]
        public Vector2 viewportOutsideBorders = new(-0.02f, 1.02f);

        [Tooltip("outermost boundary for for infinity screen translation \n\n Must be bigger than spawn offset")]
        public float screenInfinityOutsideOffset = 0.3f;
        [Tooltip("outermost boundary for objects - used for spawn objects \n\n Must be smaller tan infinity offset")]
        public float screenSpawnOutsideOffset = 0.25f;
    }
}