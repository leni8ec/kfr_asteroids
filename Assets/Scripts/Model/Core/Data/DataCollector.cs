using Model.Core.Data.State.Base;
using Model.Core.Unity.Data;

namespace Model.Core.Data {
    public class DataCollector {

        public StatesCollector States { get; }
        public ConfigsCollector Configs { get; }

        public DataCollector(StatesCollector statesCollector, ConfigsCollector configsCollector) {
            this.States = statesCollector;
            this.Configs = configsCollector;
        }
    }
}