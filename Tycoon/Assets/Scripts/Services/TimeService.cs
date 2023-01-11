using System;
using Cysharp.Threading.Tasks;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Services
{
    public class TimeService : Service, ITimeService
    {
        private bool enable = false;
        private float lastTime;
        private float deltaTime;
        
        protected override void Initialize()
        {
            Update();
        }

        public async void Update()
        {
            lastTime = Time.time;
            while (enable)
            {
                OnUpdate?.Invoke();
                deltaTime = Time.time - lastTime;
                await UniTask.DelayFrame(0);
                
            }
        }

        public event Action OnUpdate;
    }
}