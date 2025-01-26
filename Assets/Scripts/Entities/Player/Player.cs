using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PlayerMovements))]
public class Player : Entity
{
    [SerializeField] InputManager input;
    protected PlayerMovements movementHandler;

    protected void Awake()
    {
        movementHandler = GetComponent<PlayerMovements>();
    }
    protected override void Start()
    {
        base.Start();

        GameManager.OnPlayerJoin(this);

        //--//
        //healthSystem.ResetHealth(45);
        //moveSpeed = 2;
        //damagerModifier = 50;
        //areaSize = 0;
    }

    void FixedUpdate()
    {
        if (GameManager.currentState != GameState.InGame)
            return;

        movementHandler.MovePlayer(input.movementInput.normalized * speedModifier);
    }

    protected override void HandleDeath()
    {
        GameManager.SetGameState(GameState.GameOver);
    }
}
