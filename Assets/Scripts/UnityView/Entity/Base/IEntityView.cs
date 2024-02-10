using Model.Core.Interface.Entity;
using UnityEngine;

namespace UnityView.Entity.Base {
    public interface IEntityView {

        GameObject GameObject { get; }
        Transform Transform { get; }

        void Create(IEntity entity);

    }
}