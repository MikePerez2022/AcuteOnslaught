using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    private PoolManager pm;

    [SerializeField] private List<Weapon> equippedGuns = new();

    private List<bool> firingGuns = new();

    private bool shooting = false;


    private void Start()
    {
        foreach (Weapon weapon in equippedGuns)
        {
            firingGuns.Add(false);
        }

        pm = PoolManager.instance;
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
        int burstCount = 0;
        float currentAngle = 0;
        float anglePerBullet = 0;
        firingGuns[index] = true;

        if (weapon.SpreadAmount > 1)
        {
            anglePerBullet = weapon.SpreadAngle / (weapon.SpreadAmount - 1);
        }

        while (true)
        {
            currentAngle = weapon.SpreadAngle * .5f;

            for (int i = 0; i < weapon.SpreadAmount; i++)
            {

                Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y);
                Quaternion spawnRot = transform.rotation * Quaternion.Euler(0, 0, currentAngle + weapon.AddedAngle + Random.Range(weapon.RandomAngle.x, weapon.RandomAngle.y));

                GameObject spawned = pm.GetPooledObject(weapon.poolType, weapon.Bullet, spawnPos, spawnRot);
                Bullet bullet = spawned.GetComponent<Bullet>();

                if (bullet.usedPreviously)
                {
                    spawned.transform.SetLocalPositionAndRotation(spawnPos, spawnRot);
                }
                bullet.SetStats(weapon);
                bullet.SetInUse(gameObject);
                currentAngle -= anglePerBullet;
            }

            burstCount++;
            if (weapon.BurstAmount > 0 && burstCount < weapon.BurstAmount)
            {
                yield return new WaitForSeconds(weapon.BurstSpeed);
            }
            else
                break;

        }
        
        yield return new WaitForSeconds(weapon.FireRate + Random.Range(.01f, .05f));

        if (shooting)
        {
            firingGuns[index] = true;
            StartCoroutine(SpawnBullet(weapon, index));
        }
        else if (!shooting)
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
