using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.State;
using UnityEngine;
using UnityView.Objects.Base;
using Random = System.Random;

namespace UnityView.Objects {
    public class AsteroidView : EntityView<Asteroid, AsteroidState, AsteroidConfig> {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;


        private void Start() {
            int index = new Random().Next(sprites.Length);
            spriteRenderer.sprite = sprites[index];
        }

        protected override void SubscribeEvents() { }

    }
}