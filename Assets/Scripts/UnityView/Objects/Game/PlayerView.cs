using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Unity.Data.Config;
using UnityEngine;
using UnityView.Objects.Game.Base;

namespace UnityView.Objects.Game {
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