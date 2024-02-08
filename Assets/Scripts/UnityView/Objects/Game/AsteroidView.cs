using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Unity.Data.Config;
using UnityEngine;
using UnityView.Objects.Game.Base;
using Random = System.Random;

namespace UnityView.Objects.Game {
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