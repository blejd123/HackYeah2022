using System;
using Audio;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Inject] private readonly SceneChanger _sceneChanger;
        [Inject] private readonly SoundController _soundController;
        
        [SerializeField] private Button _play;
        [SerializeField] private Button _tutorial;
        [SerializeField] private Button _exit;
        [SerializeField] private AudioClip _music;
        [SerializeField] private GameObject _tutorialRoot;
        [SerializeField] private Button _exitTutorial;

        private void OnEnable()
        {
            _tutorialRoot.SetActive(false);
            
            _play.onClick.AddListener(OnPlayClick);
            _tutorial.onClick.AddListener(OnTutorialClick);
            _exit.onClick.AddListener(OnExitClick);
            _exitTutorial.onClick.AddListener(OnExitTutorialClick);
            
            _soundController.PlayMusic(_music, true);
        }

        private void OnDisable()
        {
            _play.onClick.RemoveListener(OnPlayClick);
            _tutorial.onClick.RemoveListener(OnTutorialClick);
            _exit.onClick.RemoveListener(OnExitClick);
            _exitTutorial.onClick.RemoveListener(OnExitTutorialClick);
        }

        private void OnPlayClick()
        {
            _sceneChanger.ChangeScene("Gameplay");
        }

        private void OnTutorialClick()
        {
            _tutorialRoot.SetActive(true);
        }

        private void OnExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnExitTutorialClick()
        {
            _tutorialRoot.SetActive(false);
        }
    }
}
