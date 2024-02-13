using Model.Core.Data.Collectors;
using UnityEngine;
using UnityView.Data;

namespace UnityView.Base {
    public class MonoBase : MonoBehaviour {
        protected static SceneData SceneData => SceneData.self;
        protected static StateCollector States => SceneData.States;
    }
}