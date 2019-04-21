using Assets.Scripts.Static;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.SceneObjects.Cylinder
{
    public class CylinderFacade : MonoBehaviour, ICylinderFacade
    {
        public Transform Transform { get { return transform; } }

        public bool Enable { get { return enabled; } set { enabled = value; } }

        public GameObject GameObject { get { return gameObject; } }

        [SerializeField]
        private Renderer m_Renderer;

        private Material m_GameOverMaterial;
        private Material m_DefaultMaterial;        

        public void SetGameOver()
        {
            m_Renderer.material = m_GameOverMaterial;            
        }        

        public void Init(Material defaultMaterial, Material gameOverMaterial)
        {            
            m_GameOverMaterial = gameOverMaterial;
            m_Renderer.material = defaultMaterial;            
        }

        public class CustomFactory : IFactory<ICylinderFacade>
        {
            [Inject]
            private GameSettings m_GameSettings;

            [Inject]
            private CylinderSettings m_CylinderSettings;

            public ICylinderFacade Create()
            {
                GameObject cylinder = GameObject.Instantiate(m_GameSettings.CylinderPrefab);
                ICylinderFacade cylinderFacade = cylinder.GetComponent<ICylinderFacade>();
                int materialIndex = Random.Range(0, m_CylinderSettings.DefaultTextures.Length);
                cylinderFacade.Init(m_CylinderSettings.DefaultTextures[materialIndex], m_CylinderSettings.GameOverTexture);
                return cylinderFacade;
            }
        }
    }
}
