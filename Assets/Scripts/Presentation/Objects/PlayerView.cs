using Core.Config;
using Core.Objects;
using Core.State;
using Presentation.Objects.Base;
using UnityEngine;

namespace Presentation.Objects {
    public class PlayerView : EntityView<Player, PlayerState, PlayerConfig> {
        [Space]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite idleSprite;
        [SerializeField] private Sprite moveSprite;
        [Space]
        [SerializeField] private AudioSource moveAudio;

        protected override void SubscribeEvents() {
            State.MoveState.Changed += OnMoveStateChanged;
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