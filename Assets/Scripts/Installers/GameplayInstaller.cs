using Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        [SerializeField] private GameplayController _gameplayController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Podium _podium;
        [SerializeField] private ObstacleTrack _obstacleTrack;
        [SerializeField] private Audience _audience;
        [SerializeField] private InputController _InputController;

        public override void InstallBindings()
        {
            InstallDarek();
            InstallMateusz();
        }

        private void InstallDarek()
        {
            Container.Bind<GameplayController>().FromInstance(_gameplayController).AsSingle().NonLazy();
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
            Container.Bind<Podium>().FromInstance(_podium).AsSingle().NonLazy();
            Container.Bind<ObstacleTrack>().FromInstance(_obstacleTrack).AsSingle().NonLazy();
            Container.Bind<Audience>().FromInstance(_audience).AsSingle().NonLazy();
        }

        private void InstallMateusz()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ObstacleHitGiraffeSignal>();

            Container.BindFactory<Object, Obstacle, Obstacle.Factory>().FromFactory<PrefabFactory<Obstacle>>();
            Container.BindFactory<Object, GiraffeController, GiraffeController.Factory>().FromFactory<PrefabFactory<GiraffeController>>();

            Container.BindInstance(_InputController);
        }
    }
}
