using Core.Config;
using Core.Interface.Base;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Systems.Game;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class PlayerSystem : IUpdate {
        private PlayerState State { get; }
        private PlayerConfig Config { get; }
        public Player Player { get; }
        private Transform PlayerTransform { get; }

        private bool active;

        public PlayerSystem(PlayerState state, ConfigCollector configCollector, PrefabCollector prefabCollector) {
            State = state;
            Config = configCollector.player;

            // Player
            Player = CreatePlayer(prefabCollector.player, configCollector.player);
            PlayerTransform = Player.transform;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
            Player.gameObject.SetActive(true);
        }

        private void Reset() {
            active = false;
            Player.Reset();
            State.Reset();
            Player.gameObject.SetActive(false);
        }


        private Player CreatePlayer(GameObject playerPrefab, PlayerConfig playerConfig) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = playerObject.GetComponent<Player>();
            targetPlayer.SetConfig(playerConfig);
            targetPlayer.SetStateData(State);
            return targetPlayer;
        }


        public void Upd(float deltaTime) {
            if (!active) return;

            // Moving
            if (State.MoveState.Value) {
                if (State.inertialTime < 1) {
                    State.inertialTime = Mathf.Min(1, State.inertialTime + deltaTime * (1 / Config.accelerationInertia));
                    State.inertialSpeed = Mathf.Lerp(0, Config.speed, State.inertialTime);
                }
            } else {
                if (State.inertialTime > 0) {
                    State.inertialTime = Mathf.Max(0, State.inertialTime - deltaTime * (1 / Config.brakingInertia));
                    State.inertialSpeed = Mathf.Lerp(0, Config.speed, State.inertialTime);
                }
            }

            if (State.inertialTime > 0) {
                // transform.Translate(transform.up * (config.speed * deltaTime));
                Vector3 direction;
                if (State.MoveState.Value) {
                    direction = Vector3.Lerp(State.lastDirection, PlayerTransform.up, deltaTime / Config.leftOverInertia); // leftover inertia
                } else {
                    direction = State.lastDirection; // don't change direction without acceleration
                }
                State.lastDirection = direction * State.inertialTime;

                Vector3 position = PlayerTransform.position;
                position += direction * (State.inertialSpeed * deltaTime);
                PlayerTransform.position = position;
            }

            // Rotation
            if (State.RotateState.Value != 0) {
                PlayerTransform.Rotate(0, 0, Config.rotationSpeed * deltaTime * State.RotateState.Value);
            }


            // Calculate speed
            Vector3 pos = PlayerTransform.position;
            State.speed = Vector3.Distance(State.lastPos, pos) / Time.deltaTime;
            State.lastPos = pos;
        }
    }
}