using Model.Core.Adapters.Base;
using UnityEngine;

namespace Model.Core.Adapters {
    public interface IAudioAdapter : IAdapter {

        void PlaySound(AudioClip clip);

    }
}