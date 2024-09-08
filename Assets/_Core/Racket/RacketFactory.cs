using UnityEngine;

namespace Arkanoid.Racket
{
    public class RacketFactory
    {
        private readonly PrefabRepository _prefabRepository;

        public RacketFactory(PrefabRepository prefabRepository)
        {
            _prefabRepository = prefabRepository;
        }

        public Racket Create(Field field)
        {
            Racket prefab = _prefabRepository.Racket;
            Racket racket = Object.Instantiate(prefab, field.transform);
            Bounds fieldBounds = field.Bounds;
            racket.transform.localPosition = new Vector2(
                fieldBounds.center.x,
                fieldBounds.center.y - fieldBounds.extents.y + racket.Bounds.extents.y);
            return racket;
        }
    }
}
