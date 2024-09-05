using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [CreateAssetMenu]
    public class LevelRepository : ScriptableObject
    {
        [field: SerializeField]
        public List<Level> Levels { get; private set; }
    }
}
