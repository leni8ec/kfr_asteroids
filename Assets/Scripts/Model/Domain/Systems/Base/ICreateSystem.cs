namespace Model.Domain.Systems.Base {
    public interface ICreateSystem : ISystem {

        /// <summary>
        /// Called after all systems constructors is called (when objects are created)
        /// </summary>
        void Create();

    }
}