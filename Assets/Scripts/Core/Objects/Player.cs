using Core.Config;
using Core.Interface.Objects;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Player : Entity<PlayerConfig>, IPlayer {
        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite idleSprite;
        public Sprite moveSprite;
        [Space]
        public AudioSource moveAudio;

        private PlayerState State { get; set; }

        public override float Radius => config.colliderRadius;

        // position of bullet start
        public Vector3 WeaponWorldPosition => transform.position + transform.up * 0.2f;


        public void SetStateData(PlayerState state) {
            State = state;
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

        public override void Reset() {
            base.Reset();
            State.Reset();

            Transform t = transform;
            t.position = Vector3.zero;
            t.eulerAngles = Vector3.zero;
        }

    }
}