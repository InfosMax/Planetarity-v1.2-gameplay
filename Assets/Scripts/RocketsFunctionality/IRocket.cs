using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.RocketsFunctionality
{
    public interface IRocket
    {
        Dictionary<RocketProperties, int> Parameters { get; }

        void Launch(Vector3 dirrection);
        int CalculateDamage();
    }
}


