using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Tags
{
    public class TagCollection : MonoBehaviour
    {
        [SerializeField]
        private List<ObjectTag> _tags = new();

        public bool Contains(ObjectTag objectTag)
        {
            return _tags.Contains(objectTag);
        }
    }
}
