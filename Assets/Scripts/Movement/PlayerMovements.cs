using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovements : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    /// <summary>
    /// Handle the movements of the player with the given direction
    /// </summary>
    /// <param name="_movement">movement direction</param>
    public void MovePlayer(Vector2 _movement)
    {
        rb.linearVelocity = _movement;
    }
}
