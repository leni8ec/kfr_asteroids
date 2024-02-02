namespace Core.Data {
    public class DataCollector {
        public ScoreData Score { get; } = new();
        public PlayerData Player { get; } = new();
        public WorldData World { get; } = new();
        public GameStateData GameState { get; } = new();
    }
}