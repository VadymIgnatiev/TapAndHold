using System;
using UnityEngine;

namespace Assets.Scripts.SceneObjects.Camera
{
    [Serializable]
    public class CameraSettings
    {        
        public Vector3 InitialPosition;
        public float FollowTowerSpeed;
        public float CameraFieldofView;
        public float ShowTowerTime;
    }
}
