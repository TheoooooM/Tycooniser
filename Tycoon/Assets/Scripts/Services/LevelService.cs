using System.Collections.Generic;
using Services.Interfaces;
using UnityEngine;
using static AdresseHelper;

namespace Services
{
    public class LevelService : Service, ILevelService
    {
        private MapData map;
        private Vector3 spawnPosition;
        private Vector3 leavePosition;
        private Dictionary<Enums.Interaction, List<InterestPoint>> interactionInterestPoints;

        protected override void Initialize()
        {
            LoadAssetWithCallback<GameObject>("Map", GenerateMap);
        }
        
        void GenerateMap(GameObject obj)
        {
            var mapGO = Object.Instantiate(obj);
            mapGO.transform.position = Vector3.zero;
            map = mapGO.GetComponent<MapData>();
            spawnPosition = map.spawnPoint.position;
            leavePosition = map.leavePoint.position;
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
            return spawnPosition;
        }

        public Vector3 GetLeavePosition()
        {
            return leavePosition;
        }
    }
}