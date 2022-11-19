using UnityEngine;

namespace Gameplay
{
    public class ObstacleTrack : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;

        private Obstacle _currentObstacle;

        public void SetupObstacle(Obstacle obstaclePrefab)
        {
            _currentObstacle = Instantiate(obstaclePrefab);
            _currentObstacle.transform.position = _start.position;
        }

        public void DestroyObstacle()
        {
            if (_currentObstacle != null)
            {
                Destroy(_currentObstacle.gameObject);
                _currentObstacle = null;
            }
        }
        
        public void AnimateObstacle(float time)
        {
            
        }
    }
}
