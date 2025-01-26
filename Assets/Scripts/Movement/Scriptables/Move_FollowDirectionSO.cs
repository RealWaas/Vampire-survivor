using UnityEngine;

[CreateAssetMenu(fileName = "MovementBehaviour", menuName = "Movement Behaviour/Follow direction")]
public class Move_FollowDirectionSO : MovementBehaviourSO
{
    public override void Move(Transform _entityTransform, Transform _target, float _moveSpeed)
    {
        // See if possible to have a différent Vector3 for each entity
        
        // Vector3 targetDirection = _target.position - _entityTransform.position;
        // _entityTransform.position = Vector3.MoveTowards(
        //    _entityTransform.position,
        //    targetDirection * 10,
        //    _moveSpeed * Time.deltaTime
        //);
    }
}