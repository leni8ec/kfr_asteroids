using Model.Data.State;
using Model.Data.State.Base;
using Presenter.Collectors.Base;

namespace Presenter.Collectors {
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