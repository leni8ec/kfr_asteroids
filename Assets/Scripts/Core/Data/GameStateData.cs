using Core.Base;
using Core.Game.States;

namespace Core.Data {
    public class GameStateData {
        public ChangedValue<GameState> GameState { get; } = new();
    }
}