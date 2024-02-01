using UnityEngine;

namespace Presentation.GUI {
    public class BgAudio : MonoBehaviour {
        public float beatRate = 0.3f;
        private float beatCountdown;
        private bool beatFlag;

        public AudioSource beat1Audio;
        public AudioSource beat2Audio;

        private void Update() {
            if ((beatCountdown -= Time.deltaTime) > 0) return;
            beatCountdown = beatRate;

            if (beatFlag) beat1Audio.Play();
            else beat2Audio.Play();
            beatFlag = !beatFlag;
        }

    }
}