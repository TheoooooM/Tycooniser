using Attributes;
using Services.Interfaces;

namespace Services
{
    public class GameService : IGameService
    {
        [DependeOnService] 
        private ITimeService timeService;

        public int E = 3;
    }
}