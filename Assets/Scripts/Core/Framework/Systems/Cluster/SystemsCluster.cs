using System;
using System.Collections.Generic;

namespace Core.Framework.Systems.Cluster {

    public class SystemsCluster : ISystemsCluster {
        private readonly Dictionary<Type, ISystem> systems;
        private readonly List<IUpdateSystem> updateSystems;

        public SystemsCluster() {
            systems = new Dictionary<Type, ISystem>();
            updateSystems = new List<IUpdateSystem>();

            Initialization();
        }

        public void Initialization() {
            foreach (ISystem system in systems.Values) system.Initialize();
        }

        public void Add<T>(T system) where T : ISystem {
            // Fill systems
            systems.Add(typeof(T), system);
            // Fill update
            if (system is IUpdateSystem updateSystem) updateSystems.Add(updateSystem);
        }

        public void Upd(float deltaTime) {
            foreach (IUpdateSystem updateSystem in updateSystems) {
                if (updateSystem.Active) updateSystem.Upd(deltaTime);
            }
        }

    }
}