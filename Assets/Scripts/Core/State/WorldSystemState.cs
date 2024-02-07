using Core.Interface.State;

namespace Core.State {
    public class WorldSystemState : IStateData {

        public float asteroidSpawnCountdown;
        public float ufoSpawnCountdown;


        public void Reset() {
            asteroidSpawnCountdown = default;
            ufoSpawnCountdown = default;
        }

    }
}