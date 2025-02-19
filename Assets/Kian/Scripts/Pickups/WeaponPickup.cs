using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weaponToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Tried trigger");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.TryGetComponent(out ShootingManager sm))
            {
                sm.AddGun(weaponToGive);
                Destroy(gameObject);
            }


        }
    }
}
