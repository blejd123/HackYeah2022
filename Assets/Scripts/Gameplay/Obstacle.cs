using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Obstacle : MonoBehaviour
    {
        [Inject] private SignalBus _SignalBus;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GiraffeController>())
            {
                Debug.Log("obstacle hit giraffe!");
                _SignalBus.Fire(new ObstacleHitGiraffeSignal());
            }
        }

        public sealed class Factory : PlaceholderFactory<Object, Obstacle>
        {
        }
    }
}
