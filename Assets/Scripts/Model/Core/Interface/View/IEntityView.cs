using Model.Core.Interface.Objects;
using UnityEngine;

namespace Model.Core.Interface.View {
    public interface IEntityView {

        public IEntity EntityLink { get; }

        GameObject GameObject { get; }
        Transform Transform { get; }

    }
}