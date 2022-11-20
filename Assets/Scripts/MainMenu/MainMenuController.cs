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

        private void OnEnable()
        {
            _play.onClick.AddListener(OnPlayClick);
            _tutorial.onClick.AddListener(OnTutorialClick);
            _exit.onClick.AddListener(OnExitClick);
            
            _soundController.PlayMusic(_music, true);
        }

        private void OnDisable()
        {
            _play.onClick.RemoveListener(OnPlayClick);
            _tutorial.onClick.RemoveListener(OnTutorialClick);
            _exit.onClick.RemoveListener(OnExitClick);
        }

        private void OnPlayClick()
        {
            _sceneChanger.ChangeScene("Gameplay");
        }

        private void OnTutorialClick()
        {
            
        }

        private void OnExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
