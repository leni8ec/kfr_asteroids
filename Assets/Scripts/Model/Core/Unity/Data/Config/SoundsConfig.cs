using Model.Core.Interface.Config;
using UnityEngine;

namespace Model.Core.Unity.Data.Config {
    [CreateAssetMenu(menuName = "Data/SoundsData")]
    public class SoundsConfig : ScriptableObject, IConfigData {
        public AudioClip playerMove;
        [Space]
        public AudioClip fire1;
        public AudioClip fire2;
        [Space]
        public AudioClip ufoLarge;
        public AudioClip ufoSmall;
        [Space]
        public AudioClip explosionLarge;
        public AudioClip explosionMedium;
        public AudioClip explosionSmall;
        [Space]
        public AudioClip extraShip;
        [Space]
        public AudioClip wavyBit1;
        public AudioClip wavyBit2;
    }
}