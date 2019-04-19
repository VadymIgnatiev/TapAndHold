using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SceneObjects.Camera
{
    public class CameraFacade : MonoBehaviour//, ISceneCamera
    {        
        private Transform m_TargetTransform;
        private float m_RotationSpeed;
        private Vector3 m_Offset;

        //[Inject]
        //private CameraSettings m_CameraSettings;

        //void Start()
        //{
        //    m_RotationSpeed = m_CameraSettings.RotationSpead;
        //    SetCameraToTarget();
        //}

        //private void SetCameraToTarget()
        //{
        //    float x = m_TargetTransform.position.x;
        //    float y = m_TargetTransform.position.y;
        //    float z = m_TargetTransform.position.z - m_CameraSettings.CameraOffset;

        //    transform.position = new Vector3(x, y, z);

        //    m_Offset = m_TargetTransform.position - transform.position;
        //}

        //public void SetTargetTransform(Transform targetTransform)
        //{
        //    m_TargetTransform = targetTransform;
        //    SetCameraToTarget();
        //}

        //public void LateUpdate()
        //{
        //    float horizontal = 0;
        //    if (Input.GetMouseButton(0))
        //    {
        //        horizontal = Input.GetAxis("Mouse X") * m_RotationSpeed;
        //    }

        //    m_TargetTransform.Rotate(0, horizontal, 0);

        //    float desiredAngle = m_TargetTransform.eulerAngles.y;
        //    Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        //    transform.position = m_TargetTransform.position - (rotation * m_Offset);

        //    transform.LookAt(m_TargetTransform);
        //}
    }
}

