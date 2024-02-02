using Core.Data;
using Core.Objects;
using Core.Unity.Helpers;
using UnityEngine;

namespace Core.Unity {
    [DefaultExecutionOrder(-100)]
    public class SceneData : MonoBehaviourHandler<SceneData> {
        public Camera mainCamera;
        [Space]
        public ConfigCollector configCollector;
        public PrefabCollector prefabCollector;

        public Player Player { get; private set; }
        public DataCollector Data { get; private set; }

        public void SetGameData(DataCollector data) {
            Data = data;
        }

        public void SetPlayer(Player player) {
            Player = player;
        }
    }
}