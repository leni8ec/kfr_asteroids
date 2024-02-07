using Model.Core.Game;
using Model.Core.Interface.State;
using Model.Core.State.Base;

namespace Model.Core.State {
    public class GameSystemState : IStateData {

        public ValueChange<bool> ContinueFlag { get; } = new();
        public ValueChange<GameStatus> Status { get; } = new();


        public void Reset() {
            ContinueFlag.Reset();
            Status.Reset();
        }

    }
}