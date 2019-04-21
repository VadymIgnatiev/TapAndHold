using System.Collections.Generic;

namespace Assets.Scripts.SceneObjects.Cylinder
{
    public interface ICylinderPool
    {
        ICylinderFacade GetCylinder();
        void PutCylinderToPool(List<ICylinderFacade> cylinderFacades);
    }
}
