using System.Collections.Generic;
using Services.Interfaces;
using UnityEngine;
using static AdresseHelper;

namespace Services
{
    public class LevelService : Service, ILevelService
    {
        private MapData map;
        private Transform spawnPoint;
        private Transform leavePoint;
        public Transform entryPoint;
        public Transform exitPoint;
        private Dictionary<Enums.Interaction, List<InterestPoint>> interactionInterestPoints;

        protected override void Initialize()
        {
            LoadAssetWithCallback<GameObject>("Map", GenerateMap);
        }
        
        void GenerateMap(GameObject obj)
        {
            var mapGO = Object.Instantiate(obj);
            gameObjectDependency.Add(mapGO);
            mapGO.transform.position = Vector3.zero;
            map = mapGO.GetComponent<MapData>();
            spawnPoint = map.spawnPoint;
            leavePoint = map.leavePoint;
            entryPoint = map.entryPoint;
            exitPoint = map.exitPoint;
            foreach (var interestPoint in map.interestPoints)
            {
                foreach (var interaction in interestPoint.interactions)
                {
                    List<InterestPoint> interestPoints= interactionInterestPoints.ContainsKey(interaction)? interactionInterestPoints[interaction] : new List<InterestPoint>();
                    interestPoints.Add(interestPoint);
                    interactionInterestPoints[interaction] = interestPoints;
                }
            }
        }

        public Vector3 GetSpawnPosition()
        {
            return spawnPoint.position;
        }

        public Vector3 GetLeavePosition()
        {
            return leavePoint.position;
        }
    }
}