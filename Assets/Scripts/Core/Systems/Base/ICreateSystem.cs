namespace Core.Systems.Base {
    public interface ICreateSystem : ISystem {

        /// <summary>
        /// Called after all systems constructors is called (when objects are created)<br/><br/>
        /// Called before `Start()`
        /// </summary>
        void Create();

    }
}