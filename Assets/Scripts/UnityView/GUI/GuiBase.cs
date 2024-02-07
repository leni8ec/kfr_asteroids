using Model.Core.State;
using Model.Core.Unity;
using UnityEngine;

namespace UnityView.GUI {
    public class GuiBase : MonoBehaviour {
        protected static SceneData SceneData => SceneData.self;
        protected static StateCollector States => SceneData.State;
    }
}