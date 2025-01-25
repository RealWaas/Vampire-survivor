using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public Entity GetClosestEnemy(Vector3 _targetPos, float _range, LayerMask _mask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_targetPos, _range, _mask);

        if (colliders.Length == 0) return null;

        return colliders.OrderBy(entity =>
        {
            return Vector3.Distance(entity.transform.position, _targetPos);
        }).FirstOrDefault().GetComponent<Entity>();
    }

    public List<Entity> GetEnemiesInRange(Vector3 _targetPos, float _range, LayerMask _mask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_targetPos, _range, _mask);

        if (colliders.Length == 0) return null;

        List<Entity> entitiesInRange = new List<Entity>();
        foreach (Collider2D collider in colliders)
        {
            entitiesInRange.Add(collider.GetComponent<Entity>());
        }

        return entitiesInRange;
    }
}
