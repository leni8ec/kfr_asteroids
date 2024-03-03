namespace Control.Collectors {
    // todo: it's a god object for EntityView (presentation layer)
    public class GameDataContainer {

        public ConfigCollector Configs { get; }
        public StateCollector States { get; }

        public GameDataContainer(ConfigCollector configsCollector, StateCollector statesCollector) {
            Configs = configsCollector;
            States = statesCollector;
        }
    }
}