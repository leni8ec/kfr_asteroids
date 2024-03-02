using System;
using Model.Core.Data.State.Base;

namespace Model.Core.Data.State {
    public class WeaponSystemState : IStateData {

        #region Input state

        /// <summary>
        /// State - Weapon
        /// </summary>
        public Weapon fireStatus;

        #endregion

        public float fire1Countdown;
        public float fire2Countdown;

        // Laser data
        public float laserShotCountdownDuration;
        public float laserShotsCount;


        public void Reset() {
            fireStatus = default;
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