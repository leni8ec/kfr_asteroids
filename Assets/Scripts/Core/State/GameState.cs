using Core.Base;
using Core.Game;

namespace Core.State {
    public class GameState {
        public ChangedValue<GameStatus> Status { get; } = new();



    }
}