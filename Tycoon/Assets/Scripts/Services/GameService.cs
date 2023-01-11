using Attributes;
using Services.Interfaces;
using UnityEngine;

namespace Services
{
    public class GameService : Service, IGameService
    {
        [DependeOnService] 
        private ITimeService timeService;

        [InitializedOnCompose]
        protected override void Initialize()
        {
            Debug.Log("EE : " + timeService);
        }

        
    }
}