using Attributes;
using Services.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using static AdresseHelper;

namespace Services
{
    public class EntityService : Service
    {
        [DependeOnService] 
        private IPoolService poolService;
        [DependeOnService] 
        private ILevelService levelService;

        private GameObject basicEntity;
        
        protected override void Initialize()
        {
            LoadAssetWithCallback<GameObject>("BasicEntity",GenerateEntity);
        }

        void GenerateEntity(GameObject obj)
        {
            basicEntity = obj;
            poolService.PoolInstantiate(basicEntity, levelService.GetSpawnPosition(), Quaternion.identity);
            
        }
    }
}