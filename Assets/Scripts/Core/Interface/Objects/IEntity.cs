using Core.Interface.Base;
using UnityEngine;

namespace Core.Interface.Objects {

    public interface IEntity : IDestroy, IReset {

        GameObject GameObject { get; }
        Transform Transform { get; }

    }

}