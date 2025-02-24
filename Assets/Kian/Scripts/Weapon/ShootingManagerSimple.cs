using System.Collections;
using UnityEngine;

public class ShootingManagerSimple : MonoBehaviour
{
    public IEnumerator SpawnBullet(Weapon weapon, int index)
    {
        int burstCount = 0;
        float currentAngle = 0;
        float anglePerBullet = 0;

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
                GameObject spawned = PoolManager.instance.GetPooledObject(weapon.poolType, weapon.Bullet, spawnPos, spawnRot);
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
            if (burstCount < weapon.BurstAmount)
            {
                yield return new WaitForSeconds(weapon.BurstSpeed);
            }
            else
                break;

        }


        yield return null;
    }
}
