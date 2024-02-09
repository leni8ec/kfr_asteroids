using Model.Core.Interface.Entity;
using UnityEngine;

namespace Model.Core.Interface.View {
    public interface IEntityView {

        GameObject GameObject { get; }
        Transform Transform { get; }

        void Create(IEntity entity);

    }
}