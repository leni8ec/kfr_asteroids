using Model.Adapters;
using UnityEngine;
using UnityView.Base;

namespace UnityView.Adapters {
    public class UnityAudioAdapter : MonoBase, IAudioAdapter {

        [SerializeField] private AudioSource audioSource;

        public void PlaySound(AudioClip clip) {
            if (!clip) {
                Debug.LogError("AudioClip is null!");
                return;
            }

            audioSource.PlayOneShot(clip);
        }

    }
}