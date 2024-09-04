using UnityEngine;

namespace Arkanoid
{
    public class Block : MonoBehaviour
    {
        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}
