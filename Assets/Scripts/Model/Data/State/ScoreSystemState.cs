using Model.Data.State.Base;

namespace Model.Data.State {
    public class ScoreSystemState : IStateData {

        public ValueChange<int> Points { get; } = new();

        public void Reset() {
            Points.Reset();
        }

    }
}