using UnityEngine;

[CreateAssetMenu(fileName = "MovementBehaviour", menuName = "Movement Behaviour/Follow target")]
public class Move_FollowTargetSO : MovementBehaviourSO
{
    public override void Move(Transform _entityTransform, Transform _target, float _moveSpeed)
    {
        _entityTransform.position = Vector3.MoveTowards(
           _entityTransform.position,
           _target.position,
           _moveSpeed * Time.deltaTime
       );
    }
}