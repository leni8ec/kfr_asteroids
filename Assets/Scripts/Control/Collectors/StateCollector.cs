using Control.Collectors.Base;
using Model.Core.Data.State;
using Model.Core.Data.State.Base;

namespace Control.Collectors {
    public class StateCollector : CollectorBase<IStateData> {

        public StateCollector() {
            Add(new ActiveEntitiesState());
            Add(new EntitiesManagersState());
            Add(new EnemiesSystemState());
            Add(new GameSystemState());
            Add(new ScoreSystemState());
            Add(new WeaponSystemState());
        }

    }
}