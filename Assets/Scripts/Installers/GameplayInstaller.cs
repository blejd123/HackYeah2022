using Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        [SerializeField] private CameraController _cameraController;

        public override void InstallBindings()
        {
            InstallDarek();
            InstallMateusz();
        }

        private void InstallDarek()
        {
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
        }

        private void InstallMateusz()
        {
            
        }
    }
}
