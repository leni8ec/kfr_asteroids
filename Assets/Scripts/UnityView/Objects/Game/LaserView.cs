using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Unity.Data.Config;
using UnityEngine;
using UnityView.Objects.Game.Base;

namespace UnityView.Objects.Game {
    public class LaserView : EntityView<Laser, LaserAmmoState, LaserConfig> {
        [SerializeField] private Transform scaledTransform;
        [SerializeField] private SpriteRenderer laserSprite;


        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() {
            // Set laser scale (visual)
            Vector3 scale = scaledTransform.localScale;
            scale.y = Config.maxDistance;
            scaledTransform.localScale = scale;
        }

        private void Update() {
            Color color = laserSprite.color;
            color.a = Mathf.Max(0, State.duration / Config.duration);
            laserSprite.color = color;
        }

    }
}