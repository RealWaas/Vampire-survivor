using System.Collections;
using UnityEngine;

public class RotatingController : AttackController
{
    protected float speed;
    [SerializeField] protected float rotatespeed = 100;

    [SerializeField] protected GameObject projectilePrefab;

    //Todo:
    //Set the weapon to modify the radius of the controller


    protected override void OnTriggerEnter2D(Collider2D collision) { }

    public override void InitializeAttack(WeaponSystem _system)
    {
        base.InitializeAttack(_system);
        speed = _system.weaponData.levelStats[_system.level].baseProjectileSpeed * _system.bearer.projectileSpeedModifier;

        int projectileCount = _system.weaponData.levelStats[_system.level].baseCount + _system.bearer.countBonus;

        StartCoroutine(spawnProjectiles(projectileCount, _system.weaponData.attackOffset, _system));
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.forward * rotatespeed * speed * Time.deltaTime);
    }

    IEnumerator spawnProjectiles(int _projectileCount, Vector3 _offset, WeaponSystem _system)
    {
        float timeBetweenSpawns = 360f / ((rotatespeed * speed) * _projectileCount);

        for (int countIndex = 0; countIndex < _projectileCount; countIndex++)
        {
            Vector3 spawnPosition = this.transform.position + _offset * areaSize;

            // Instantiate the projectile at the calculated position
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, projectilePrefab.transform.rotation);

            // Optionally, parent the projectile to the rotating object
            projectile.transform.SetParent(this.transform, worldPositionStays: true);

            if (projectile.TryGetComponent(out AttackController attackController))
                attackController.InitializeAttack(_system);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
