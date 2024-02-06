using Core.Base;
using Core.Interface.State;

namespace Core.State {
    public class ScoreState : IStateData {

        public ValueChange<int> Points { get; } = new();

        public void Reset() {
            Points.Reset();
        }

    }
}