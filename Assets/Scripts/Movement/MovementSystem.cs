using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Handle the movements of an object by giving it a behaviour.
/// </summary>
public class MovementSystem : MonoBehaviour
{
    [SerializeField] MovementBehaviourSO movementBehaviour;

    private Transform targetTransform;
    private float moveSpeed;

    /// <summary>
    /// Initialize the movement system to follow its behaviour at a given speed.
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_moveSpeed"></param>
    public void InitSystem(Transform _target, float _moveSpeed)
    {
        targetTransform = _target;
        moveSpeed = _moveSpeed;
    }

    private void Update()
    {
        if (!targetTransform && !movementBehaviour) return;

        movementBehaviour.Move(transform, targetTransform, moveSpeed);
    }
}