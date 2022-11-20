using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI
{
    public class SceneChanger : MonoBehaviour
    {
        [Inject] private readonly Fader _fader;

        public void ChangeScene(string sceneName)
        {
            StartCoroutine(ChangeSceneCoroutine(sceneName));
        }

        private IEnumerator ChangeSceneCoroutine(string sceneName)
        {
            yield return _fader.FadeIn().WaitForCompletion();
            SceneManager.LoadScene(sceneName);
            yield return _fader.FadeOut().WaitForCompletion();
        }
    }
}
