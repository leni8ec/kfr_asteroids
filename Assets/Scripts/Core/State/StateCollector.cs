namespace Core.State {
    public class StateCollector {
        public ScoreState Score { get; } = new();
        public PlayerState Player { get; } = new();
        public WorldState World { get; } = new();
        public GameState Game { get; } = new();
    }
}