using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class Player : Entity
{
    public static Player instance;
    [SerializeField] InputManager input;

    private GameObject playerRenderer;
    private Animator animator;

    private ExpSystem expSystem;

    private CharacterDataSO characterData;

    private Vector3 originalPosition = Vector3.zero;

    protected override void Awake()
    {
        instance = this;

        base.Awake();
        originalPosition = transform.position;
        GameManager.OnGameReset += ResetCharacter;
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

        animator = playerRenderer.transform.GetChild(0).GetComponent<Animator>();

        // Reset player stats
        stats = _entityData.baseStats;
        healthSystem.ResetHealth(BASE_HEALTH * stats.healthModifier);
        expSystem.ResetExp();

        // Add the starting weapon
        ItemManager.UpdateItem(characterData.startingWeapon);

        isAlive = true;
    }

    public void UpdateStats(EntityStats _modifier)
    {
        stats = stats.ApplyStatsModifier(_modifier);
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            movementHandler.ResetMovement();
            return;
        }

        if (input.movementInput != Vector2.zero)
            animator.SetBool("1_Move", true);
        else
            animator.SetBool("1_Move", false);


        if(input.movementInput.x >= 0)
            playerRenderer.transform.localScale = new Vector3(-1, 1, 1);
        else
            playerRenderer.transform.localScale = new Vector3(1, 1, 1);

        movementHandler.MoveEntity(input.movementInput.normalized * BASE_SPEED * stats.speedModifier);
    }

    public void CollectExp(float _amount) => expSystem.GainExp(_amount);
    public void GainHeal(float _amount) => healthSystem.TakeHeal(_amount);

    protected override void TakeDamage(float _knockBack) { }

    protected override void HandleDeath()
    {
        isAlive = false;
        animator.SetTrigger("4_Death");
        StartCoroutine(GameOverCoroutine());
    }

    private void ResetCharacter()
    {
        StopAllCoroutines();
        characterData = null;

        // Remove the player renderer
        Destroy(playerRenderer);

        animator = null;

        // Add the starting weapon
        ItemManager.ResetWeapons();

        transform.position = originalPosition;

        isAlive = false;
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(3f);
        GameManager.SetGameState(GameState.GameOver);
    }
}
