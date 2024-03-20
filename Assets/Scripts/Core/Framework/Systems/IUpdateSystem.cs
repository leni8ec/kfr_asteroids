namespace Core.Framework.Systems {
    public interface IUpdateSystem : ISystem {

        /// <summary>
        /// Called every frame when system is Active
        /// </summary>
        void Upd(float deltaTime);

    }
}