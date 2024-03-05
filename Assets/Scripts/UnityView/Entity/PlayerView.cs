using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity;
using UnityEngine;
using UnityView.Entity.Base;

namespace UnityView.Entity {
    public class PlayerView : EntityView<Player, PlayerState, PlayerConfig> {
        [Space]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite idleSprite;
        [SerializeField] private Sprite moveSprite;
        [Space]
        [SerializeField] private AudioSource moveAudio;

        protected override void SubscribeEvents() {
            State.Move.Changed += OnMoveStateChanged;
        }

        private void OnMoveStateChanged(bool moveFlag) {
            if (moveFlag) {
                moveAudio.Play();
                spriteRenderer.sprite = moveSprite;
            } else {
                moveAudio.Stop();
                spriteRenderer.sprite = idleSprite;
            }
        }

    }
}