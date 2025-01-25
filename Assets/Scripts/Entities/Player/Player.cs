using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class Player : Entity
{
    public static Player Instance;

    [SerializeField] InputManager input;

    protected void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();

        //--//
        healthSystem.ResetHealth(45);
        moveSpeed = 2;
        damage = 50;
    }

    void FixedUpdate()
    {
        if (GameManager.currentState != GameState.InGame)
            return;

        movementHandler.MoveEntity(input.movementInput.normalized * moveSpeed);
    }

    protected override void HandleDeath()
    {
        GameManager.SetGameState(GameState.GameOver);
    }
}
