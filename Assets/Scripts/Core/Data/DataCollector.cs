namespace Core.Data {
    public class DataCollector {
        public ScoreData Score { get; } = new();
        public WorldData World { get; } = new();
        public PlayerData Player { get; } = new();
    }
}