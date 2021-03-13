using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.GameManagement
{
    public interface ILevelGenerator
    {
        void Generate(int enemiesMinNumber, int enemiesMaxNumber);
    }
}
