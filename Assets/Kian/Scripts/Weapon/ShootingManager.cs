using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private PoolManager pm;

    [SerializeField] private List<Weapon> equippedGuns = new();

    private List<bool> firingGuns = new();

    private bool shooting = false;


    private void Start()
    {
        foreach (Weapon weapon in equippedGuns)
        {
            firingGuns.Add(false);
        }
    }

    public void Shoot(bool shoot)
    {
        shooting = shoot;

        if (shooting)
        {
            for (int i = 0; i < equippedGuns.Count; i++) 
            {
                if (!firingGuns[i])
                    StartCoroutine(SpawnBullet(equippedGuns[i], i));
            }
        }
    }

    public IEnumerator SpawnBullet(Weapon weapon, int index)
    {
        firingGuns[index] = true;

        Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y);

        GameObject spawned = pm.GetPooledObject(weapon.poolType, weapon.Bullet, spawnPos, transform.rotation);
        Bullet bullet = spawned.GetComponent<Bullet>();

        if (bullet.usedPreviously)
        {
            spawned.transform.SetLocalPositionAndRotation(spawnPos, transform.rotation);
        }

        bullet.SetInUse();

        yield return new WaitForSeconds(weapon.FireRate);

        if (shooting)
        {
            firingGuns[index] = true;
            StartCoroutine(SpawnBullet(weapon, index));
        }
        else
        {
            firingGuns[index] = false;
            yield return null;
        }
    }

    public void AddGun(Weapon weapon)
    {
        equippedGuns.Add(weapon);
        firingGuns.Add(false);

        Shoot(shooting);
    }
}
