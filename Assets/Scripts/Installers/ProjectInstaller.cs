using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
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
