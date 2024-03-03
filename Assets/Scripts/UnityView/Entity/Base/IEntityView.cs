using Model.Entity.Interface;
using UnityEngine;

namespace UnityView.Entity.Base {
    public interface IEntityView {

        GameObject GameObject { get; }
        Transform Transform { get; }

        void Create(IEntity entity);

    }
}