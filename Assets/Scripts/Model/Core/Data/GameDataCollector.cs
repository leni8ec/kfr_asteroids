using Model.Core.Data.Collectors;

namespace Model.Core.Data {
    public class GameDataCollector {

        public ConfigCollector Configs { get; }
        public StateCollector States { get; }

        public GameDataCollector(ConfigCollector configsCollector, StateCollector statesCollector) {
            Configs = configsCollector;
            States = statesCollector;
        }
    }
}