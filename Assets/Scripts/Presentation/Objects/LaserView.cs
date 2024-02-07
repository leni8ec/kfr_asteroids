﻿using Core.Config;
using Core.Objects;
using Core.State;
using Presentation.Objects.Base;
using UnityEngine;

namespace Presentation.Objects {
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