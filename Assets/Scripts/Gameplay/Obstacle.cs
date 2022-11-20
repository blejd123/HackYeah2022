using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Obstacle : MonoBehaviour
    {
        [Inject] private SignalBus _SignalBus;

        [SerializeField] private Texture2D _outline;

        public Texture2D Outline => _outline;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Giraffe"))
            {
                var root = other.gameObject.GetComponentInParent<GiraffeController>();
                foreach (var rigidbody in root.GetComponentsInChildren<Rigidbody>())
                {
                    rigidbody.constraints = RigidbodyConstraints.None;
                    rigidbody.useGravity = true;
                }
                foreach (var collider in root.GetComponentsInChildren<Collider>())
                {
                    collider.isTrigger = false;
                }
                
                _SignalBus.Fire(new ObstacleHitGiraffeSignal());
            }
        }

        public sealed class Factory : PlaceholderFactory<Object, Obstacle>
        {
        }
    }
}
