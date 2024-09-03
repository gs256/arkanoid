using Unity.VisualScripting;

namespace Arkanoid.Tags
{
    public static class TagUtils
    {
        public static bool HasTag(this UnityEngine.Object unityObject, ObjectTag objectTag)
        {
            TagCollection tagCollection = unityObject.GetComponent<TagCollection>();

            if (!tagCollection)
                return false;

            return tagCollection.Contains(objectTag);
        }
    }
}
