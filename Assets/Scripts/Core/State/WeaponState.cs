using System;
using Core.Base;
using Core.Interface.State;

namespace Core.State {
    public class WeaponState : IStateData {

        #region Input state

        /// <summary>
        /// State - Weapon
        /// </summary>
        public ValueChange<Weapon> FireState { get; } = new();

        #endregion

        public float fire1Countdown;
        public float fire2Countdown;

        // Laser data
        public float laserShotCountdownDuration;
        public float laserShotsCount;


        public void Reset() {
            FireState.Reset();
            fire1Countdown = default;
            fire2Countdown = default;
            laserShotCountdownDuration = default;
            laserShotsCount = default;
        }


        [Flags]
        public enum Weapon {
            Empty = 0,
            Gun = 1 << 0,
            Laser = 1 << 1
        }

    }
}