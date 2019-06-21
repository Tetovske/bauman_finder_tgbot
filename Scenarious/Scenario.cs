using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonFinder.src.Scenarious
{
    /// <summary>
    /// Базовый абстрактный класс для всех сценариев
    /// </summary>
    public abstract class Scenario
    {
        bool isScenarioActive;
        public abstract void BeginScenario();
        public abstract void KillScenario();

        public abstract event Action OnScenarioBegin, OnScenarioEnded;
    }
}
