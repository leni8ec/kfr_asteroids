using Core.Base;
using Core.Game;

namespace Core.State {
    public class GameState : IStateData {
        public ChangedValue<GameStatus> Status { get; } = new();


    }
}