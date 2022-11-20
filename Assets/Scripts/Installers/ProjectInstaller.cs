using Audio;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private Fader _fader;
        [SerializeField] private SoundController _soundController;
        
        public override void InstallBindings()
        {
            InstallDarek();
            InstallMateusz();
        }

        private void InstallDarek()
        {
            Container.BindInterfacesAndSelfTo<Fader>().FromInstance(_fader).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneChanger>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundController>().FromInstance(_soundController).AsSingle().NonLazy();
        }

        private void InstallMateusz()
        {

        }
    }
}
