using Assets.Scripts.SceneObjects.Camera;
using Assets.Scripts.SceneObjects.Cylinder;
using Assets.Scripts.Static;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ZenjectInstallers
{
    [CreateAssetMenu(fileName = "ProjectSettingsInstaller", menuName = "Create ProjectSettings")]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        public CameraSettings CameraSettings;
        public GameSettings GameSettings;
        public CylinderSettings CylinderSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(CameraSettings).IfNotBound();
            Container.BindInstance(GameSettings).IfNotBound();
            Container.BindInstance(CylinderSettings).IfNotBound();
        }
    }
}
