using System.Collections.Generic;
using UnityEngine;

namespace Planetarity.GameManagement
{
    public interface ILevelGenerator
    {
        List<GameObject> Generate(int enemiesMinNumber, int enemiesMaxNumber);
        List<GameObject> Generate(int enemiesNumber);
    }
}
