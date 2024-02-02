using Core.Base;
using Core.Game;

namespace Core.State {
    public class GameState : IStateData {
        public ValueChange<GameStatus> Status { get; } = new();


    }
}