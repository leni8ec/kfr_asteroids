using Model.Data.Reactive;
using Model.Data.State.Base;

namespace Model.Data.State {
    public class ScoreSystemState : IStateData {

        public ReactiveProperty<int> Points { get; } = new();

        public void Reset() {
            Points.Reset();
        }

    }
}