using Assets.Scripts.Static;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.SceneObjects.Camera
{
    public class CameraFacade : MonoBehaviour, ICameraFacade
    {        
        [Inject]
        private CameraSettings m_CameraSettings;

        [Inject]
        private GameSettings m_GameSettings;

        private float m_RoundStep;
        private Vector3 m_InitPosition;
        private float m_CameraFieldofView;
        private float m_FollowTowerSpeed;
        private Vector3 m_NextStepPosition;
        private Vector3 m_ShowTowerPosition;
        private float m_ShowTowerTime;
        private float m_ShowTowerSpeed;
        public bool m_NeedMoveCamera;
        public bool m_NeedShowTower;
        public bool m_NeedSetToInitPosition;

        void Start()
        {
            m_RoundStep = m_GameSettings.RoundStep;
            m_InitPosition = m_CameraSettings.InitialPosition;
            m_CameraFieldofView = m_CameraSettings.CameraFieldofView;
            m_FollowTowerSpeed = m_CameraSettings.FollowTowerSpeed;
            m_ShowTowerTime = m_CameraSettings.ShowTowerTime;
            SetToInitPosition();
        }

        public void Update()
        {
            if (m_NeedMoveCamera)
                MoveCameraByTower();

            if (m_NeedShowTower)
                MoveToShowCamera();

            if (m_NeedSetToInitPosition)
                MoveToInitPosition();
        }

        public void SetToInitPosition()
        {
            m_NeedSetToInitPosition = true;
            m_NeedShowTower = false;
            m_NeedMoveCamera = false;
        }

        public void MoveToInitPosition()
        {
            transform.position = m_InitPosition;
            m_NeedSetToInitPosition = false;
            Debug.Log("m_InitPosition" + m_InitPosition);
        }

        public void MoveToOneStep()
        {
            m_NextStepPosition = new Vector3(transform.position.x, transform.position.y + m_RoundStep, transform.position.z);
            
            m_NeedMoveCamera = true;
        }                        

        public void MoveCameraByTower()
        {
            transform.position = Vector3.Lerp(transform.position, m_NextStepPosition, m_FollowTowerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_NextStepPosition) < 0.001f)
                m_NeedMoveCamera = false;
        }

        public void ShowTower()
        {
            m_NeedShowTower = true;
            float height = (transform.position.y - m_InitPosition.y) / 2;
            float distance = height / Mathf.Tan(Mathf.Deg2Rad * m_CameraFieldofView / 2);

            m_ShowTowerPosition = new Vector3(m_InitPosition.x, m_InitPosition.y + height, m_InitPosition.z - distance);

            m_ShowTowerSpeed = (transform.position - m_ShowTowerPosition).magnitude / m_ShowTowerTime;
        }

        private void MoveToShowCamera()
        {
            transform.position = Vector3.Lerp(transform.position, m_ShowTowerPosition, m_ShowTowerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_ShowTowerPosition) < 0.001f)
                m_NeedShowTower = false;
        }
        
    }
}

