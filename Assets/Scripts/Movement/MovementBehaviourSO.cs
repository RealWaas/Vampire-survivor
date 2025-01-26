using UnityEngine;

public abstract class MovementBehaviourSO : ScriptableObject
{
    public abstract void Move(Transform _entityTransform, Transform _target, float _moveSpeed);
}