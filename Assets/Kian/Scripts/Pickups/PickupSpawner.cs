using UnityEngine;

public abstract class PickupSpawner : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] protected float pickupDropChance;
    [SerializeField] protected GameObject pickupObject;

    public abstract void SpawnPickup();
}
