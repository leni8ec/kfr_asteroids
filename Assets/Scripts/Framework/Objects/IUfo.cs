using UnityEngine;

namespace Framework.Objects {
    public interface IUfo : IEntity {

        /**
         * Stay on target and go to him
         */
        void Hunt(Transform target);

    }
}