using UnityEngine;

namespace Services.Interfaces
{
    public interface ILevelService : IService
    {
        Vector3 GetSpawnPosition();
        Vector3 GetLeavePosition();
    }
}