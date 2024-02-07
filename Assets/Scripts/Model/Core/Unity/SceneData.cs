﻿using Model.Core.State;
using Model.Core.Unity.Helpers;
using UnityEngine;

namespace Model.Core.Unity {
    [DefaultExecutionOrder(-100)]
    public class SceneData : MonoBehaviourHandler<SceneData> {
        public Camera mainCamera;
        [Space]
        public ConfigCollector configCollector;
        public PrefabCollector prefabCollector;

        public StateCollector State { get; private set; }

        public void SetGameData(StateCollector state) {
            State = state;
        }

    }
}