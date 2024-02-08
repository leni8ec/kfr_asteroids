using System;
using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State;
using Model.Core.Game;
using Model.Core.Interface.Objects;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
    public class GameStateSystem : SystemBase {
        private GameSystemState State { get; }

        // Events
        public static event Action NewGameEvent;
        public static event Action GameOverEvent;


        public GameStateSystem(DataCollector data, AdaptersCollector adapters) {
            State = data.States.game;

            CollisionSystem.PlayerHit += PlayerHitHandler;
            State.ContinueFlag.Changed += ContinueFlagChangedHandler;

            NewGame();
        }

        private void ContinueFlagChangedHandler(bool toContinue) {
            if (toContinue) NewGame();
            State.ContinueFlag.Value = false;
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void NewGame() {
            if (State.Status.Value == GameStatus.Playing) return;
            State.Status.Value = GameStatus.Playing;
            NewGameEvent?.Invoke();
            Debug.Log("NewGame");
        }

        private void GameOver() {
            if (State.Status.Value == GameStatus.End) return;
            State.Status.Value = GameStatus.End;
            GameOverEvent?.Invoke();
            Debug.Log("GameOver");
        }
    }
}