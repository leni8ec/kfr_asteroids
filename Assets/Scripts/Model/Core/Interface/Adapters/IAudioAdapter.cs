using Model.Core.Interface.Adapters.Base;
using UnityEngine;

namespace Model.Core.Interface.Adapters {
    public interface IAudioAdapter : IAdapter {

        void PlaySound(AudioClip clip);

    }
}