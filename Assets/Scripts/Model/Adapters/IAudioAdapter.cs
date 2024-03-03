using Model.Adapters.Base;
using UnityEngine;

namespace Model.Adapters {
    public interface IAudioAdapter : IAdapter {

        void PlaySound(AudioClip clip);

    }
}