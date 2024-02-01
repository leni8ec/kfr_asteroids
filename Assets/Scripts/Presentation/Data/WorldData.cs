using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/WorldData")]
    public class WorldData : ScriptableObject {
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
        public Vector2 viewportOutsideBorders = new(-0.1f, 1.1f);
    }
}