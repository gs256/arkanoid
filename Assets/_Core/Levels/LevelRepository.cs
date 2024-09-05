using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Levels
{
    [CreateAssetMenu]
    public class LevelRepository : ScriptableObject
    {
        [field: SerializeField]
        public List<Level> Levels { get; private set; }
    }
}
