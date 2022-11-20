using System;
using UI;
using UnityEngine;
using Zenject;

namespace Init
{
    public class InitSceneController : MonoBehaviour
    {
        [Inject] private readonly SceneChanger _sceneChanger;

        private void Start()
        {
            _sceneChanger.ChangeScene("MainMenu");
        }
    }
}
