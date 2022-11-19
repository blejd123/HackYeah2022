using System.Collections;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [Inject] private readonly CameraController _cameraController;

        private void Start()
        {
            StartCoroutine(RunGameplaySequence());
        }

        private IEnumerator RunGameplaySequence()
        {
            yield return ShowCasters();
            yield return ShowGiraffePreObstacle();
            yield return ShowObstacle();
            yield return ShowGiraffe();
        }

        private IEnumerator ShowCasters()
        {
            yield break;
        }
        
        private IEnumerator ShowGiraffePreObstacle()
        {
            yield break;
        }
        
        private IEnumerator ShowObstacle()
        {
            yield break;
        }
        
        private IEnumerator ShowGiraffe()
        {
            yield break;
        }
    }
}
