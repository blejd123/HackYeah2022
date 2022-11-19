using Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Podium _podium;
        [SerializeField] private ObstacleTrack _obstacleTrack;

        public override void InstallBindings()
        {
            InstallDarek();
            InstallMateusz();
        }

        private void InstallDarek()
        {
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
            Container.Bind<Podium>().FromInstance(_podium).AsSingle().NonLazy();
            Container.Bind<ObstacleTrack>().FromInstance(_obstacleTrack).AsSingle().NonLazy();
        }

        private void InstallMateusz()
        {
            
        }
    }
}
