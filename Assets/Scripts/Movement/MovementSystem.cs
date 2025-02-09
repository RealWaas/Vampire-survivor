using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementSystem : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    /// <summary>
    /// Handle the movements of the player with the given direction
    /// </summary>
    /// <param name="_direction">movement direction</param>
    public void MoveEntity(Vector2 _direction)
    {
        rb.linearVelocity = _direction;
    }

    public void ResetMovement()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
