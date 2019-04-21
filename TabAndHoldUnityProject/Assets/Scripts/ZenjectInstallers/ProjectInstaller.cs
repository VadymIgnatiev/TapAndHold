using Assets.Scripts.Scene;
using Assets.Scripts.SceneObjects.Camera;
using Assets.Scripts.SceneObjects.Cylinder;
using Assets.Scripts.UI.Touch;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {            
            Container.Bind<ICameraFacade>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ITouchFacade>().To<TouchFacade>().AsSingle();
            Container.Bind<SceneManager>().AsSingle();
            Container.BindFactory<ICylinderFacade, CylinderFactory>().FromFactory<CylinderFacade.CustomFactory>();
            Container.Bind<ICylinderPool>().To<CylinderPool>().AsSingle();
            Container.Bind<AsyncProcessor>().FromComponentInHierarchy().AsSingle();
        }
    }
}
