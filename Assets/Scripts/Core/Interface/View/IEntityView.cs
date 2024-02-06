using Core.Interface.Objects;
using UnityEngine;

namespace Core.Interface.View {
    public interface IEntityView {

        public IEntity Entity { get; }

        GameObject GameObject { get; }
        Transform Transform { get; }

    }
}