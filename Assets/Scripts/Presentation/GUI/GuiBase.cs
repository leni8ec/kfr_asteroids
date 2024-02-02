using Core.State;
using Core.Unity;
using UnityEngine;

namespace Presentation.GUI {
    public class GuiBase : MonoBehaviour {
        protected static SceneData SceneData => SceneData.Handler;
        protected static StateCollector State => SceneData.State;
    }
}