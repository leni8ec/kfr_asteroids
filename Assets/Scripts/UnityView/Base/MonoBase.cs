using Model.Core.Data.State.Base;
using UnityEngine;
using UnityView.Data;

namespace UnityView.Base {
    public class MonoBase : MonoBehaviour {
        protected static SceneData SceneData => SceneData.self;
        protected static StatesCollector States => SceneData.States;
    }
}