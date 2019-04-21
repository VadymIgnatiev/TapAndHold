using System;
using UnityEngine;

namespace Assets.Scripts.Static
{
    [Serializable]
    public class GameSettings
    {
        public float RoundStep;
        public float ExtensionSpeed;
        public GameObject CylinderPrefab;
        public GameObject TowerBase;
        public float CylinderPrecision;
        public float GameOverLimit;
        public float GameOverDestroyTime;
    }
}
