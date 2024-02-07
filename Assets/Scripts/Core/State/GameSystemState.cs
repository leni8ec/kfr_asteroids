using Core.Base;
using Core.Game;
using Core.Interface.State;

namespace Core.State {
    public class GameSystemState : IStateData {

        public ValueChange<bool> ContinueFlag { get; } = new();
        public ValueChange<GameStatus> Status { get; } = new();


        public void Reset() {
            ContinueFlag.Reset();
            Status.Reset();
        }

    }
}