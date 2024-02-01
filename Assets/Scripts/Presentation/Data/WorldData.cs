using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/WorldData")]
    public class WorldData : ScriptableObject {
        [Tooltip("Count in sec")]
        public float asteroidsSpawnRate = 3;
        [Tooltip("Count in sec")]
        public float ufoSpawnRate = 0.1f;

        public Vector2 viewportOutsideBorders = new(-0.1f, 1.1f);
    }
}