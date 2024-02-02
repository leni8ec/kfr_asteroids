using UnityEngine;

namespace Presentation.Config {
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerConfig : ScriptableObject {
        public float speed = 3f;
        [Tooltip("in sec to full speed")]
        public float accelerationInertia = 0.5f;
        public float brakingInertia = 5f;
        public float leftOverInertia = 2f;
        [Tooltip("Degrees in sec")]
        public float rotationSpeed = 180;

        [Header("Collision")]
        public float colliderRadius = 0.1f;
    }

}