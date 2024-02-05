using Core.Base;
using Core.Game;
using Core.State.Base;

namespace Core.State {
    public class GameState : IStateData {

        public ValueChange<bool> ContinueFlag { get; } = new();
        public ValueChange<GameStatus> Status { get; } = new();


        public void Reset() {
            ContinueFlag.Reset();
            Status.Reset();
        }

    }
}