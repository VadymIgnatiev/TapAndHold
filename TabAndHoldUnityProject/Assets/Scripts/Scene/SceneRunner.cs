using UnityEngine;
using Zenject;

namespace Assets.Scripts.Scene
{
    public class SceneRunner : MonoBehaviour
    {
        [Inject]
        private SceneManager m_SceneManager;

        public void Start()
        {
            m_SceneManager.StartManager();
        }
        
    }
}
