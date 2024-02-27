using Model.Core.Data.Collectors;

namespace Model.Core.Data {
    // todo: it's a god object for EntityView (presentation layer)
    public class GameDataCollector {

        public ConfigCollector Configs { get; }
        public StateCollector States { get; }

        public GameDataCollector(ConfigCollector configsCollector, StateCollector statesCollector) {
            Configs = configsCollector;
            States = statesCollector;
        }
    }
}