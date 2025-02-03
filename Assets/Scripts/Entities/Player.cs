using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(MovementSystem))]
public class Player : Entity
{
    public static Player instance;
    [SerializeField] InputManager input;
    protected MovementSystem movementHandler;
    private ExpSystem expSystem;

    private CharacterDataSO characterData;
    private GameObject characterRenderer;


    protected void Awake()
    {
        instance = this;
        movementHandler = GetComponent<MovementSystem>();
        expSystem = GetComponent<ExpSystem>();
    }
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Set the stats, starting weapon and renderer.
    /// </summary>
    /// <param name="_entityData"></param>
    public override void SetStats(EntityDataSO _entityData)
    {
        base.SetStats(_entityData);

        characterData = (CharacterDataSO)_entityData;
        characterRenderer = characterData.characterRender;

        AttackManager.UpdateWeapon(characterData.startingWeapon);
    }

    void FixedUpdate()
    {
        if (GameManager.currentState != GameState.InGame)
            return;

        movementHandler.MoveEntity(input.movementInput * speedModifier);

        //Vector3 inputDir = new Vector3(input.movementInput.x, input.movementInput.y, 0);
        //transform.position += inputDir.normalized * speedModifier * Time.deltaTime;
    }

    public void CollectExp(float _amount) => expSystem.GainExp(_amount);

    protected override void HandleDeath()
    {
        GameManager.SetGameState(GameState.GameOver);
    }
}
