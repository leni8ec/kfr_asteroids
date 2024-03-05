using Model.Data.Reactive;
using Model.Data.State.Base;
using Model.Game;

namespace Model.Data.State {
    public class GameSystemState : IStateData {

        public ReactiveProperty<bool> ContinueFlag { get; } = new();
        public ReactiveProperty<GameStatus> Status { get; } = new();


        public void Reset() {
            ContinueFlag.Reset();
            Status.Reset();
        }

    }
}