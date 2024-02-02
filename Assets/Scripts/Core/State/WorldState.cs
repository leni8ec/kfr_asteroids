namespace Core.State {
    public class WorldState {
        public float asteroidSpawnCountdown;
        public float ufoSpawnCountdown;

        public readonly float disposeInterval = 1;
        public float disposeCountdown = 10;

        public readonly int asteroidDestroyFragments = 4;
    }
}