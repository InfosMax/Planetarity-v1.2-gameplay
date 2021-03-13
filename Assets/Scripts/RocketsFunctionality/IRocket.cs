using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocket
    {
        Dictionary<RocketProperties, float> Parameters { get; }

        void Fly();
        float CalculateDamage();
    }
}


