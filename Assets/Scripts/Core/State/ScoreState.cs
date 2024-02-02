using Core.Base;

namespace Core.State {
    public class ScoreState {

        public ChangedValue<int> Points { get; } = new();

    }
}