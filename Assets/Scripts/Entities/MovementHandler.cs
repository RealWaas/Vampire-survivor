using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementHandler : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform entityRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveEntity(Vector2 _movement)
    {
        rb.linearVelocity = _movement;

        Quaternion rotation = entityRenderer.transform.rotation;
        if (rb.linearVelocityX > 0 )
        {
            rotation.y = 180;
        } else if (rb.linearVelocityX < 0)
        {
            rotation.y = 0;
        }
        entityRenderer.transform.rotation = rotation;
    }
}
