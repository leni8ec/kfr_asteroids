using Model.Core.Data.State.Base;
using Model.Core.Unity.Data;

namespace Model.Core.Data {
    public class DataCollector {

        public StatesCollector States { get; }
        public ConfigsCollector Configs { get; }
        public PrefabsCollector Prefabs { get; }

        public DataCollector(StatesCollector statesCollector, ConfigsCollector configsCollector, PrefabsCollector prefabsCollector) {
            this.States = statesCollector;
            this.Configs = configsCollector;
            this.Prefabs = prefabsCollector;
        }
    }
}