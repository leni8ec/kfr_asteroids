namespace Core.State {
    // todo: implement DI
    public class StateCollector {

        public readonly ObjectsState objects = new();
        public readonly WorldSystemState world = new();

        public readonly GameSystemState game = new();
        public readonly ScoreState score = new();

        public readonly WeaponSystemState weapon = new();

    }
}