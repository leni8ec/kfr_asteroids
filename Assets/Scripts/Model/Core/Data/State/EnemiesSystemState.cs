using Model.Core.Data.State.Base;

namespace Model.Core.Data.State {
    public class EnemiesSystemState : IStateData {

        public float asteroidSpawnCountdown;
        public float ufoSpawnCountdown;


        public void Reset() {
            asteroidSpawnCountdown = default;
            ufoSpawnCountdown = default;
        }

    }
}