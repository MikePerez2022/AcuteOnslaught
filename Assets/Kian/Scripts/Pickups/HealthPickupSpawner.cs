using System.Collections.Generic;
using UnityEngine;

public class HealthPickupSpawner : PickupSpawner
{

    public override void SpawnPickup()
    {
        if (pickupObject == null) return;

        float randCheck = Random.value;

        if (randCheck <= pickupDropChance)
        {
            GameObject spawned = Instantiate(pickupObject, transform.position, transform.rotation);
        }
    }
}
