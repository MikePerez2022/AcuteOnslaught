using System.Collections.Generic;
using UnityEngine;

public class HealthPickupSpawner : PickupSpawner
{
    public static HealthPickupSpawner instance;

    [SerializeField] private float basicHealthAmount;
    [SerializeField] private float rareHealthAmount;

    [SerializeField] private Color basicColor;
    [SerializeField] private Color rareColor;

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
                spawned.GetComponentInChildren<SpriteRenderer>().color = rareColor;
            }
            else
            {
                hp.healthToGive = basicHealthAmount;
                spawned.GetComponentInChildren<SpriteRenderer>().color = basicColor;
            }
        }
    }
}
