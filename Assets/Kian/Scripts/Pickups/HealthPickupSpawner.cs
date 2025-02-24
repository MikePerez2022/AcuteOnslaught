using System.Collections.Generic;
using UnityEngine;

public class HealthPickupSpawner : PickupSpawner
{
    public static HealthPickupSpawner instance;

    [SerializeField] private float basicHealthAmount;
    [SerializeField] private float rareHealthAmount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    public override void SpawnPickup(Vector2 transform)
    {
        if (pickupObject == null) return;

        float basicCheck = Random.value;
        float rareCheck = Random.value;


        if (basicCheck <= pickupDropChance)
        {
            GameObject spawned = Instantiate(pickupObject, transform, Quaternion.identity);
            HealthPickup hp = spawned.GetComponent<HealthPickup>();

            if (rareCheck <= pickupRareDropChance)
            {
                hp.healthToGive = rareHealthAmount;
            }
            else
            {
                hp.healthToGive = basicHealthAmount;
            }
        }
    }
}
