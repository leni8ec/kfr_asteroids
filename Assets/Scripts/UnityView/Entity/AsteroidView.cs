using Model.Core.Data.State;
using Model.Core.Data.Unity.Config;
using Model.Core.Entity;
using UnityEngine;
using UnityView.Entity.Base;
using Random = System.Random;

namespace UnityView.Entity {
    public class AsteroidView : EntityView<Asteroid, AsteroidState, AsteroidConfig> {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;

        protected override void SubscribeEvents() { }

        private void Start() {
            int index = new Random().Next(sprites.Length);
            spriteRenderer.sprite = sprites[index];
        }

    }
}