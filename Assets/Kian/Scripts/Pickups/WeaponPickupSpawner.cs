using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupSpawner : PickupSpawner
{
    [SerializeField] private List<Weapon> possibleDrops = new();

    public override void SpawnPickup()
    {
        if (possibleDrops.Count == 0) return;

        float randCheck = Random.value;

        if (randCheck <= pickupDropChance)
        {
            GameObject spawned = Instantiate(pickupObject, transform.position, transform.rotation);
            if (spawned.TryGetComponent(out WeaponPickup wp))
            {
                wp.weaponToGive = possibleDrops[Random.Range(0, possibleDrops.Count - 1)];
            }
            //Instantiate(possibleDrops[Random.Range(0, possibleDrops.Count - 1)]);
        }
    }
}
