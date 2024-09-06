using UnityEngine;

namespace Arkanoid.Ui.Lives
{
    public class HeartFactory
    {
        private readonly PrefabRepository _prefabRepository;

        public HeartFactory(PrefabRepository prefabRepository)
        {
            _prefabRepository = prefabRepository;
        }

        public GameObject Create(Transform root)
        {
            return Object.Instantiate(_prefabRepository.Heart, root);
        }
    }
}
