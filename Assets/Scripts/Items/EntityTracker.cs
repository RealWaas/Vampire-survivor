using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EntityTracker
{

    private static Collider2D[] GetEnemiesInRange(Vector3 _targetPos, float _range, string _ownerTag) => Physics2D.OverlapCircleAll(_targetPos, _range, LayerMask.GetMask("Entity"));


    /// <summary>
    /// Get the closest entity in a given radius.
    /// </summary>
    /// <param name="_targetPos">origin point</param>
    /// <param name="_range">maximum range</param>
    /// <param name="_mask"></param>
    /// <returns></returns>
    public static Entity GetClosestEntity(Vector3 _targetPos, float _range, string _ownerTag)
    {
        Collider2D[] colliders = GetEnemiesInRange(_targetPos, _range, _ownerTag);

        if (colliders.Length == 0) return null;

        Collider2D enemy = colliders
            .Where(entity =>
            {
                return !entity.CompareTag(_ownerTag) && entity.TryGetComponent<Entity>(out _);
            })
            .OrderBy(entity =>
            {
                return Vector3.Distance(entity.transform.position, _targetPos);
            })
            .FirstOrDefault();

        if(enemy == null) return null;
        return enemy.GetComponent<Entity>();
    }

    public static List<Entity> GetRandomEntity(Vector3 _targetPos, float _range, string _ownerTag, int _count)
    {
        Collider2D[] colliders = GetEnemiesInRange(_targetPos, _range, _ownerTag);

        List<Collider2D> enemies = colliders
            .Where(entity =>
            {
                return !entity.CompareTag(_ownerTag) && entity.TryGetComponent<Entity>(out _);
            }).ToList();

        enemies.Shuffle();

        List<Entity> result = new List<Entity>();

        for (int index = 0; index < _count; index++)
        {
            if (index >= enemies.Count)
                break;

            result.Add(enemies[index].GetComponent<Entity>());
        }
        return result;
    }

    private static void Shuffle<T>(this List<T> _list)
    {
        int count = _list.Count;
        int last = count - 1;
        for (int index = 0; index < last; ++index)
        {
            int random = Random.Range(index, count);
            var tmp = _list[index];
            _list[index] = _list[random];
            _list[index] = tmp;
        }
    }
}
