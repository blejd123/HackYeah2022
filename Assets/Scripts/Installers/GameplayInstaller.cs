using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            InstallDarek();
            InstallMateusz();
        }

        private void InstallDarek()
        {
            
        }

        private void InstallMateusz()
        {
            
        }
    }
}
