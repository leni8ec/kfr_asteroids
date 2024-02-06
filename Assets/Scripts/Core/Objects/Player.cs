using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Player : ColliderEntity<PlayerState, PlayerConfig>, IPlayer {
        [Space]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite idleSprite;
        [SerializeField] private Sprite moveSprite;
        [Space]
        [SerializeField] private AudioSource moveAudio;

        // position of bullet start (Hack)
        public Vector3 WeaponWorldPosition => transform.position + transform.up * 0.2f;


        protected override void Initialize() {
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
            Transform t = transform;
            t.position = Vector3.zero;
            t.eulerAngles = Vector3.zero;
        }

    }
}