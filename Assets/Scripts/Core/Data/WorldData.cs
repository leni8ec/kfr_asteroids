namespace Core.Data {
    public class WorldData {
        public float asteroidSpawnCountdown;
        public float ufoSpawnCountdown;

        public readonly float disposeInterval = 1;
        public float disposeCountdown = 10;

        public readonly int asteroidDestroyFragments = 4;
    }
}