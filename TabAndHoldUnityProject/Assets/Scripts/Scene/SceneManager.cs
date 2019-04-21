using Assets.Scripts.SceneObjects.Camera;
using Assets.Scripts.SceneObjects.Cylinder;
using Assets.Scripts.Static;
using Assets.Scripts.UI.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Scene
{
    public class SceneManager
    {
        private List<ICylinderFacade> m_CylinderLine;

        [Inject]
        private ITouchFacade m_TouchFacade;

        [Inject]
        private ICylinderPool m_CylinderPool;

        [Inject]
        private GameSettings m_GameSettings;

        [Inject]
        private ICameraFacade m_CameraFacade;

        [Inject]
        private AsyncProcessor m_AsyncProcessor;

        private ICylinderFacade m_CurrentCylinderFacade;
        private Vector3 m_TopTowerPosition;
        private float m_RoundStep;
        private float m_ExtensionSpeed;
        private float m_CylinderPrecision;
        private ICylinderFacade m_TowerBase;
        private float m_GameOverDestroyTime;
        private bool m_NeedRestart;
        private bool m_NeedBlockTapUp;
        private float m_GameOverLimit; 

        public SceneManager()
        {
            m_CylinderLine = new List<ICylinderFacade>();            
        }

        public void StartManager()
        {
            m_CylinderLine = new List<ICylinderFacade>();
            m_TouchFacade.OnFingerDown += CreateNewCylinder;
            m_TouchFacade.OnFingerSet += ScaleCylinder;
            m_TouchFacade.OnFingerUp += ProcessTapUp;            
            m_RoundStep = m_GameSettings.RoundStep;
            m_TopTowerPosition = new Vector3(0, 0, 0);
            m_ExtensionSpeed = m_GameSettings.ExtensionSpeed;
            m_CylinderPrecision = m_GameSettings.CylinderPrecision;
            m_TowerBase = m_GameSettings.TowerBase.GetComponent<ICylinderFacade>();
            m_GameOverDestroyTime = m_GameSettings.GameOverDestroyTime;
            m_GameOverLimit = m_GameSettings.GameOverLimit;
        }        

        private void CreateNewCylinder()
        {
            if (m_NeedRestart) return;

            m_CurrentCylinderFacade = m_CylinderPool.GetCylinder();            
            m_TopTowerPosition.y += m_RoundStep;
            m_CurrentCylinderFacade.Transform.position = m_TopTowerPosition;
            m_CurrentCylinderFacade.Transform.localScale = new Vector3(0, 1, 0);
            m_CameraFacade.MoveToOneStep();
        }

        private void ScaleCylinder()
        {
            if (m_NeedRestart) return;

            m_CurrentCylinderFacade.Transform.localScale += new Vector3(1, 0, 1) * m_ExtensionSpeed * Time.deltaTime;

            if (m_CurrentCylinderFacade.Transform.localScale.x > m_GameOverLimit)
            {
                m_NeedBlockTapUp = true;
                m_AsyncProcessor.StartCoroutine(ProcessGameOver());
            }

        }

        private void ProcessTapUp()
        {
            if (m_NeedBlockTapUp)
            {
                m_NeedBlockTapUp = false;
                return;
            }

            if (m_NeedRestart)
            {
                Restart();
            }
            else
            {
                RoundEnd();
            }
        }

        private void RoundEnd()
        {
            ICylinderFacade previousCylinder = m_CylinderLine.Count == 0 ? m_TowerBase : m_CylinderLine[m_CylinderLine.Count - 1];

            if (previousCylinder.Transform.localScale.x < m_CurrentCylinderFacade.Transform.localScale.x)
            {                
                m_AsyncProcessor.StartCoroutine(ProcessGameOver());              
            }
            else
            {
                m_CylinderLine.Add(m_CurrentCylinderFacade);
            }            
        }        

        private IEnumerator ProcessGameOver()
        {
            m_NeedRestart = true;
            m_CurrentCylinderFacade.SetGameOver();

            yield return new WaitForSeconds(m_GameOverDestroyTime);

            GameObject.Destroy(m_CurrentCylinderFacade.GameObject);
            m_CameraFacade.ShowTower();            
        }        

        private void Restart()
        {            
            m_NeedRestart = false;
            m_CylinderPool.PutCylinderToPool(m_CylinderLine);
            m_CylinderLine.Clear();
            m_CameraFacade.SetToInitPosition();
            m_TopTowerPosition = new Vector3(0, 0, 0);
        }
        
    }
}
