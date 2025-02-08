using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class Player : Entity
{
    public static Player instance;
    [SerializeField] InputManager input;

    private GameObject playerRenderer;

    private ExpSystem expSystem;

    private CharacterDataSO characterData;

    protected override void Awake()
    {
        instance = this;

        base.Awake();
        expSystem = GetComponent<ExpSystem>();
    }

    /// <summary>
    /// Reset the stats, starting weapon and renderer of the player.
    /// </summary>
    /// <param name="_entityData"></param>
    public override void ResetEntity(EntityDataSO _entityData)
    {
        characterData = (CharacterDataSO)_entityData;

        // Replace the previous renderer so the player can change character
        Destroy(playerRenderer);
        playerRenderer = Instantiate(characterData.characterRender, transform);

        // Reset player stats
        stats = _entityData.baseStats;
        healthSystem.ResetHealth(BASE_HEALTH * stats.healthModifier);
        expSystem.ResetExp();

        // Add the starting weapon
        WeaponManager.UpdateWeapon(characterData.startingWeapon);
    }

    void FixedUpdate()
    {
        if (GameManager.currentState != GameState.InGame)
            return;

        movementHandler.MoveEntity(input.movementInput.normalized * BASE_SPEED * stats.speedModifier);
    }

    public void CollectExp(float _amount) => expSystem.GainExp(_amount);
    public void GainHeal(float _amount) => healthSystem.TakeHeal(_amount);

    protected override void TakeDamage(float _knockBack) { }

    protected override void HandleDeath()
    {
        GameManager.SetGameState(GameState.GameOver);
    }

}
