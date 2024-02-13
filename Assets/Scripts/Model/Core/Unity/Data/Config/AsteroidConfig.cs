using Model.Core.Interface.Config;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Unity.Data.Config {

    [CreateAssetMenu(menuName = "Data/AsteroidData")]
    public class AsteroidConfig : ScriptableObject, IConfigData, IColliderRadiusContainer {
        [Space]
        public Size size;
        [Space]
        public float speed = 1f;
        [Tooltip("How many new smaller fragments on destroyed")]
        public int destroyFragments = 4;

        [Header("Collision")]
        public float colliderRadius = 0.1f;

        public float ColliderRadius => colliderRadius;

        public enum Size {
            Large,
            Medium,
            Small
        }
    }

}