using Model.Core.Interface.Objects;
using UnityEngine;

namespace Model.Core.Interface.View {
    public interface IEntityView {

        GameObject GameObject { get; }
        Transform Transform { get; }

        void Create(IEntity entity);

    }
}