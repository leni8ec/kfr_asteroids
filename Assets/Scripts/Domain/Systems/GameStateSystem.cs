using System;
using Core.Game;
using Core.Interface.Objects;
using Core.State;
using Core.Unity;
using Domain.Base;
using UnityEngine;

namespace Domain.Systems {
    public class GameStateSystem : SystemBase {
        private GameSystemState State { get; }

        // Events
        public static event Action NewGameEvent;
        public static event Action GameOverEvent;


        public GameStateSystem(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            State = state.game;

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