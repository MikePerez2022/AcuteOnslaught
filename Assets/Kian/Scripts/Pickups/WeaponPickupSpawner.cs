using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupSpawner : PickupSpawner
{
    public static WeaponPickupSpawner instance;
    [SerializeField] private List<Weapon> possibleDrops = new();
    [SerializeField] private List<Weapon> possibleRareDrops = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public override void SpawnPickup(Vector2 transform)
    {
        if (possibleDrops.Count == 0) return;

        float basicCheck = Random.value;
        float rareCheck = Random.value;

        if (basicCheck <= pickupDropChance)
        {
            GameObject spawned = Instantiate(pickupObject, transform, Quaternion.identity);
            WeaponPickup wp = spawned.GetComponent<WeaponPickup>();


            if (rareCheck <= pickupRareDropChance)
            {
                wp.weaponToGive = possibleRareDrops[Random.Range(0, possibleRareDrops.Count)];
            }
            else
            {
                wp.weaponToGive = possibleDrops[Random.Range(0, possibleDrops.Count)];
            }
            //Instantiate(possibleDrops[Random.Range(0, possibleDrops.Count - 1)]);
        }
    }
}
