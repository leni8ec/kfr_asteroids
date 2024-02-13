using Model.Core.Container.Object;
using Model.Core.Data.State;
using Model.Core.Interface.State;

namespace Model.Core.Data.Collectors {
    public class StateCollector : CollectorBase<IStateData> {

        public StateCollector() {
            Add(new EntitiesState());
            Add(new WorldSystemState());
            Add(new GameSystemState());
            Add(new ScoreSystemState());
            Add(new WeaponSystemState());
        }

    }
}