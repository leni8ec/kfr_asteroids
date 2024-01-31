using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject {
        public float speed = 3f;
        [Tooltip("in sec to full speed")]
        public float inertia = 0.5f;
        [Tooltip("Degrees in sec")]
        public float rotationSpeed = 180;

        [Header("Collision")]
        public float colliderRadius = 10;
    }

}