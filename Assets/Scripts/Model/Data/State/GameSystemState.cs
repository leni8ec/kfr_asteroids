using Model.Data.State.Base;
using Model.Game;

namespace Model.Data.State {
    public class GameSystemState : IStateData {

        public ValueChange<bool> ContinueFlag { get; } = new();
        public ValueChange<GameStatus> Status { get; } = new();


        public void Reset() {
            ContinueFlag.Reset();
            Status.Reset();
        }

    }
}