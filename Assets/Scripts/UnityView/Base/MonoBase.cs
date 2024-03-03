using Control.Collectors;
using UnityEngine;
using UnityView.Collectors;

namespace UnityView.Base {
    public class MonoBase : MonoBehaviour {
        protected static SceneDataContainer SceneData => SceneDataContainer.self;
        protected static StateCollector States => SceneData.States;
    }
}