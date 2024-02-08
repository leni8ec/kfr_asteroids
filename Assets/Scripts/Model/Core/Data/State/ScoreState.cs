using Model.Core.Data.State.Base;
using Model.Core.Interface.State;

namespace Model.Core.Data.State {
    public class ScoreState : IStateData {

        public ValueChange<int> Points { get; } = new();

        public void Reset() {
            Points.Reset();
        }

    }
}