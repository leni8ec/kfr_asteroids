namespace Model.Domain.Systems.Base {
    public interface IStartSystem : ISystem {

        /// <summary>
        /// Called only once when the system is first enabled
        /// </summary>
        void Start();

    }
}