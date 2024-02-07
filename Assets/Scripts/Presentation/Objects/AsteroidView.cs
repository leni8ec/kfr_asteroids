using Core.Config;
using Core.Objects;
using Core.State;
using Presentation.Objects.Base;
using UnityEngine;
using Random = System.Random;

namespace Presentation.Objects {
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