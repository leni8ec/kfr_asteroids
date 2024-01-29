using Domain.Systems.Collision;
using Domain.Systems.Processors;
using UnityEngine;

namespace Domain {
    public class Bootstrapper : MonoBehaviour {
        private UpdateProcessor updateProcessor;
        private CollisionSystem collisionSystem;

        private void Awake() {
            updateProcessor = new UpdateProcessor();
            collisionSystem = new CollisionSystem();
        }

        // Start is called before the first frame update
        private void Start() { }

        // Update is called once per frame
        private void Update() {
            float deltaTime = Time.deltaTime;
            updateProcessor.Upd(deltaTime);
            collisionSystem.Upd(deltaTime);
        }
    }
}