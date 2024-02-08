﻿using Model.Core.Interface.State;

namespace Model.Core.Data.State.Base {
    /// <summary>
    /// Empty State. Used for new prototype objects
    /// </summary>
    public class DummyState : IStateData {

        public void Reset() { }

    }

}