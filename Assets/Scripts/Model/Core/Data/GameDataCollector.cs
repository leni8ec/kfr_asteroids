using Model.Core.Data.Collectors;

namespace Model.Core.Data {
    public class GameDataCollector {

        public ConfigCollector Configs { get; }
        public StateCollector States { get; }

        // todo: it's a god object for EntityView (presentation layer)
        public GameDataCollector(ConfigCollector configsCollector, StateCollector statesCollector) {
            Configs = configsCollector;
            States = statesCollector;
        }
    }
}