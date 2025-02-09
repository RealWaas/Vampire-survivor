using DG.Tweening;
using UnityEngine;

public abstract class BaseEnemy : Entity
{
    [SerializeField] public EnemyDataSO enemyData;
    [SerializeField] protected GameObject crystalPrefab;
    [SerializeField] protected SpriteRenderer enemyRenderer;

    [SerializeField] protected WeaponSystem weaponSystem;

    private void Start()
    {
        ResetEntity(enemyData);

        if (enemyData.startingWeapon)
        {
            weaponSystem.InitWeapon(enemyData.startingWeapon, 0, this);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }

    private void Update()
    {
        Vector2 playerDir = Player.instance.transform.position - transform.position;

        Move(playerDir);
    }

    protected virtual void Move(Vector2 _playerDir)
    {
        if (_playerDir.magnitude > 13)
        {
            SpawnManager.instance.RespawnEnemy(this.transform);
            ResetMovement();
            return;
        }
    }
    protected virtual void ResetMovement() { }

    /// <summary>
    /// Reset the health and stats of the entity.
    /// </summary>
    /// <param name="_entityData"></param>
    public override void ResetEntity(EntityDataSO _entityData)
    {
        stats = _entityData.baseStats;
        healthSystem.ResetHealth(BASE_HEALTH * stats.healthModifier);
        ResetMovement();
    }

    protected override void TakeDamage(float _knockBack)
    {
        Vector2 playerDir = Player.instance.transform.position - transform.position;

        if (_knockBack <= 0) return;
        transform.DOPunchPosition(-playerDir, _knockBack);
    }

    protected override void HandleDeath()
    {
        DropItemOnDeath();

        // make the enemy available from the pool
        gameObject.SetActive(false);
    }

    /// <summary>
    /// By default, drop an exp crystal on death.
    /// </summary>
    protected virtual void DropItemOnDeath()
    {
        // Get a crystal from the pool manager or create one
        GameObject crystal = PoolManager.GetAvailableObjectFromPool(crystalPrefab);

        if (!crystal)
        {
            // Create a crystal
            crystal = Instantiate(crystalPrefab, transform.position, Quaternion.identity);
            PoolManager.CreateObject(crystalPrefab, crystal);
        }
        else
        {
            // Respawn a crystal
            crystal.SetActive(true);
            crystal.transform.position = transform.position;
        }

        // Set the exp amount of the crystal
        crystal.GetComponent<ExpCrystal>().SetExpAmount(enemyData.expAmount);
    }
}