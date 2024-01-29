using UnityEngine;
using UnityEngine.Serialization;

namespace Domain.Data {
    [CreateAssetMenu(menuName = "Data/UfoData")]
    public class UfoData : ScriptableObject {
        [FormerlySerializedAs("speed")]
        public float startSpeed;
        public float huntSpeed;

        [Tooltip("in seconds")]
        public float huntDelay;
    }
}