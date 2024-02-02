using Core.Base;

namespace Core.State {
    public class ScoreState : IStateData {

        public ChangedValue<int> Points { get; } = new();

    }
}