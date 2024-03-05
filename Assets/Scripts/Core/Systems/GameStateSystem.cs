using System;
using Core.Systems.Base;
using Core.Systems.Interface;
using JetBrains.Annotations;
using Model.Data.State;
using Model.Entity.Interface;
using Model.Game;
using UnityEngine;

namespace Core.Systems {
    [UsedImplicitly]
    public class GameStateSystem : SystemBase, IGameStateSystem, IStartSystem {
        private GameSystemState State { get; }

        // Events
        public static event Action NewGameEvent;
        public static event Action GameOverEvent;


        public GameStateSystem(GameSystemState state) {
            State = state;

            CollisionSystem.PlayerHitEvent += PlayerHitHandler;
            State.ContinueFlag.Changed += ContinueFlagChangedHandler;
        }

        public void Start() {
            // Call "New Game" automatically on system start
            NewGame();
        }

        private void ContinueFlagChangedHandler(bool toContinue) {
            if (toContinue) NewGame();
            State.ContinueFlag.Reset();
        }

        private void PlayerHitHandler(ICollider enemy) {
            GameOver();
        }

        private void NewGame() {
            if (State.Status == GameStatus.Playing) return;
            State.Status.Value = GameStatus.Playing;
            NewGameEvent?.Invoke();
            Debug.Log("New Game");
        }

        private void GameOver() {
            if (State.Status == GameStatus.End) return;
            State.Status.Value = GameStatus.End;
            GameOverEvent?.Invoke();
            Debug.Log("Game Over");
        }

    }
}