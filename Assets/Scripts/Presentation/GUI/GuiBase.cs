using Core.Data;
using Core.Unity;
using UnityEngine;

namespace Presentation.GUI {
    public class GuiBase : MonoBehaviour {
        protected static SceneData SceneData => SceneData.Handler;
        protected static DataCollector Data => SceneData.Data;
    }
}