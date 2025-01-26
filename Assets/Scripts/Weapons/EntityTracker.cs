using System.Linq;
using UnityEngine;

public static class EntityTracker
{
    /// <summary>
    /// Get the closest entity in a given radius.
    /// </summary>
    /// <param name="_targetPos">origin point</param>
    /// <param name="_range">maximum range</param>
    /// <param name="_mask"></param>
    /// <returns></returns>
    public static Entity GetClosestEntity(Vector3 _targetPos, float _range, string _ownerTag)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_targetPos, _range, LayerMask.GetMask("Entity"));

        if (colliders.Length == 0) return null;

        return colliders
            .Where(entity =>
            {
                return entity.CompareTag(_ownerTag);
            })
            .OrderBy(entity =>
            {
                return Vector3.Distance(entity.transform.position, _targetPos);
            })
            .FirstOrDefault().GetComponent<Entity>();
    }
}
