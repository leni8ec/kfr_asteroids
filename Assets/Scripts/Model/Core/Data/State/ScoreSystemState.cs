using Model.Core.Data.State.Base;

namespace Model.Core.Data.State {
    public class ScoreSystemState : IStateData {

        public ValueChange<int> Points { get; } = new();

        public void Reset() {
            Points.Reset();
        }

    }
}