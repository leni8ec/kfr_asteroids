using Model.Core.Interface.Adapters;
using UnityEngine;
using UnityView.Base;

namespace UnityView.Adapters {
    public class UnityAudioAdapter : MonoBase, IAudioAdapter {

        [SerializeField] private AudioSource audioSource;

        public void PlaySound(AudioClip clip) {
            audioSource.PlayOneShot(clip);
        }

    }
}