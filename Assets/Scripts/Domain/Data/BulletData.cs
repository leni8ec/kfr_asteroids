using UnityEngine;

namespace Domain.Data {
    [CreateAssetMenu(menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject {
        public float speed;
        public float maxDistance;
    }

}