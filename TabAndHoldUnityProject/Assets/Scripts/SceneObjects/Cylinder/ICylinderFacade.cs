using UnityEngine;

namespace Assets.Scripts.SceneObjects.Cylinder
{
    public interface ICylinderFacade
    {
        Transform Transform { get; }
        bool Enable { get; set; }
        GameObject GameObject { get; }
        void SetGameOver();
        void Init(Material defaultMaterial, Material gameOverMaterial);
    }
}
