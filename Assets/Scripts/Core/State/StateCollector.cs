namespace Core.State {
    public class StateCollector {
        public readonly WorldState world = new();
        public readonly ObjectsState objects = new();

        public readonly GameState game = new();
        public readonly ScoreState score = new();

        public readonly PlayerState player = new();
        public readonly WeaponState weapon = new();
    }
}