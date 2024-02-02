namespace Core.State {
    public class WeaponState : IStateData {

        public bool fire1Flag;
        public bool fire2Flag;

        public float fire1Countdown;
        public float fire2Countdown;

        // Laser data
        public float laserShotCountdownDuration;
        public float laserShotsCount;

        public void Reset() {
            fire1Flag = false;
            fire2Flag = false;
            fire1Countdown = 0;
            fire2Countdown = 0;
            laserShotCountdownDuration = 0;
            laserShotsCount = 0;
        }
        
    }
}