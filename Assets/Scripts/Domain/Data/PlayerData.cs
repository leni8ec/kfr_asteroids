using UnityEngine;

namespace Domain.Data {
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject {
        public float speed;
        public float fireRate;
        public float rotationSpeed;
    }

}