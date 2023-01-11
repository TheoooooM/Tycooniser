using UnityEngine;

namespace Services.Interfaces
{
    public interface IPoolService : IService
    {
        public void PoolInstantiate(GameObject go, Vector3 position, Quaternion rotation, Transform parent = null);
    }
}