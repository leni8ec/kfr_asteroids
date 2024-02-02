using Core.Config;
using Core.Interface.Objects;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Player : Entity<PlayerConfig>, IPlayer {
        private PlayerState State { get; set; }

        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite idleSprite;
        public Sprite moveSprite;

        public override float Radius => config.colliderRadius;
        public float Speed => State.speed;

        // position of bullet start
        public Vector3 WeaponWorldPosition => transform.position + transform.up * 0.2f;

        [Space]
        public AudioSource moveAudio;

        public void SetStateData(PlayerState state) {
            State = state;
            State.MoveFlag.Changed += OnMoveFlagChanged;
        }

        private void OnMoveFlagChanged(bool moveFlag) {
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

            Transform t = transform;
            t.position = Vector3.zero;
            t.eulerAngles = Vector3.zero;

            State.Reset();
        }
        
    }
}