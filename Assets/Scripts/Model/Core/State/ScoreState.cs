using Model.Core.Interface.State;
using Model.Core.State.Base;

namespace Model.Core.State {
    public class ScoreState : IStateData {

        public ValueChange<int> Points { get; } = new();

        public void Reset() {
            Points.Reset();
        }

    }
}