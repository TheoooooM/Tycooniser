using Services.Interfaces;
using UnityEngine;

namespace Services
{
    public class PoolService : Service, IPoolService
    {
        protected override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void PoolInstantiate(GameObject go, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Object.Instantiate(go, position, rotation, parent);
        }
    }
}