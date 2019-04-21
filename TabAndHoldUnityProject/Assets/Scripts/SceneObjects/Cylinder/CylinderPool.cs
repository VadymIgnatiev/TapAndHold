using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.SceneObjects.Cylinder
{
    public class CylinderPool : ICylinderPool
    {
        [Inject]
        private CylinderFactory m_CylinderFactory;
        
        private List<ICylinderFacade> m_CylinderPool;
        private int m_PoolIndex;

        public CylinderPool()
        {
            m_CylinderPool = new List<ICylinderFacade>();
        }

        public ICylinderFacade GetCylinder()
        {
            if (m_CylinderPool.Count == 0 || m_PoolIndex >= m_CylinderPool.Count-1)
            {
                ICylinderFacade cylinderFacade = m_CylinderFactory.Create();                
                return cylinderFacade;
            }
            else
            {
                ICylinderFacade cylinderFacade = m_CylinderPool[m_PoolIndex];
                cylinderFacade.GameObject.SetActive(true);
                m_PoolIndex++;
                return cylinderFacade;
            }
        }

        public void PutCylinderToPool(List<ICylinderFacade> cylinderFacades)
        {
            for (int i = 0; i < cylinderFacades.Count; i++)
                cylinderFacades[i].GameObject.SetActive(false);

            m_CylinderPool.AddRange(cylinderFacades);
        }
    }
}
