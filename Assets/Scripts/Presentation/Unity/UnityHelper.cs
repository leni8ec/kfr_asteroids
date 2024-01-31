using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script from my projects
namespace Presentation.Unity {
    public static class UnityHelper {

        #region Detect platform

        public static readonly bool IS_EDITOR = Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor;
        public static readonly bool IS_DESKTOP_PLAYER = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXPlayer;
        public static readonly bool IS_DESKTOP = IS_EDITOR || IS_DESKTOP_PLAYER;
        public static readonly bool IS_ANDROID = Application.platform == RuntimePlatform.Android;
        public static readonly bool IS_IOS = Application.platform == RuntimePlatform.IPhonePlayer;
        public static readonly bool IS_MOBILE = IS_ANDROID || IS_IOS;
        public static bool IS_TABLET {
            get {
                // Get Device diagonal size
                float screenWidth = Screen.width / Screen.dpi;
                float screenHeight = Screen.height / Screen.dpi;
                float deviceDiagonalSizeInInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

                // Get device aspect ratio
                float deviceScreenAspectRatio = (float)Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);

                bool isTablet = deviceDiagonalSizeInInches > 6.5f && deviceScreenAspectRatio < 2f;
                return isTablet;
            }
        }

        #endregion


        /// <summary>
        /// Find components in all opened (loaded) scenes, include inactive objects.
        /// </summary>
        /// <typeparam name="T">Type of the target component</typeparam>
        /// <returns></returns>
        public static T FindComponent<T>() where T : Component {
            T result = null;

            int sceneCount = SceneManager.sceneCount;
            List<GameObject> sceneRootGameObjectsCache = new List<GameObject>();
            for (int i = 0; i < sceneCount; i++) {
                Scene scene = SceneManager.GetSceneAt(i);
                scene.GetRootGameObjects(sceneRootGameObjectsCache);
                foreach (GameObject rootGameObject in sceneRootGameObjectsCache) {
                    result = rootGameObject.transform.GetComponentInChildren<T>(true);
                    if (result) break;
                }

                if (result) break;
            }

            return result;
        }

    }
}