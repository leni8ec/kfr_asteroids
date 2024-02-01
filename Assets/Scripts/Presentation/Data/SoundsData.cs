﻿using UnityEngine;

namespace Presentation.Data {
    [CreateAssetMenu(menuName = "Data/SoundsData")]
    public class SoundsData : ScriptableObject {
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