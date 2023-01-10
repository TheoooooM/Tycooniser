using Attributes;
using Services.Interfaces;
using UnityEngine;

namespace Services
{
    public class GameService : IGameService
    {
        [DependeOnService] 
        private ITimeService timeService;

        public int E = 3;

        [InitializedOnCompose]
        void Initialize()
        {
            Debug.Log("EE : " + timeService);
        }
    }
}