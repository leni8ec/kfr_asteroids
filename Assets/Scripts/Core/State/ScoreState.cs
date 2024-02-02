using Core.Base;

namespace Core.State {
    public class ScoreState : IStateData {

        public ValueChange<int> Points { get; } = new();

    }
}