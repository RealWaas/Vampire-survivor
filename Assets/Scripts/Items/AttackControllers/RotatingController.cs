using System.Collections;
using UnityEngine;

public class RotatingController : AttackController
{
    [SerializeField] protected float rotatespeed = 100;

    [SerializeField] protected GameObject projectilePrefab;

    //Todo:
    //Set the weapon to modify the radius of the controller


    protected override void OnTriggerEnter2D(Collider2D collision) { }

    public override void InitializeAttack(WeaponSystem _system)
    {
        base.InitializeAttack(_system);

        //ParentConstraint constraint = gameObject.GetComponent<ParentConstraint>();
        //constraint.AddSource(_system.bearer.transform);

        foreach (Transform child in  transform)
        {
            child.gameObject.SetActive(false);
        }

        StartCoroutine(spawnProjectiles(weaponStats.count, _system.weaponData.attackOffset, _system));
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.forward * rotatespeed * weaponStats.speed * Time.deltaTime);
    }

    IEnumerator spawnProjectiles(int _projectileCount, Vector3 _offset, WeaponSystem _system)
    {
        float timeBetweenSpawns = 360f / ((rotatespeed * weaponStats.speed) * _projectileCount);

        for (int countIndex = 0; countIndex < _projectileCount; countIndex++)
        {
            Vector3 spawnPosition = this.transform.position + _offset * weaponStats.size;

            GameObject attack = PoolManager.GetAvailableObjectFromPool(projectilePrefab);

            if (!attack)
            {
                attack = Instantiate(projectilePrefab, spawnPosition, projectilePrefab.transform.rotation);
                PoolManager.CreateObject(projectilePrefab, attack);
            }
            else
            {
                attack.SetActive(true);
                attack.transform.position = spawnPosition;
                attack.transform.localScale = projectilePrefab.transform.localScale;
                attack.transform.rotation = projectilePrefab.transform.rotation;
            }



            // Optionally, parent the projectile to the rotating object
            attack.transform.SetParent(this.transform, worldPositionStays: true);

            if (attack.TryGetComponent(out AttackController attackController))
                attackController.InitializeAttack(_system);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
