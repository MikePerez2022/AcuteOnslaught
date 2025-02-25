using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weaponToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.TryGetComponent(out ShootingManager sm))
            {
                sm.AddGun(weaponToGive);
                AudioPlayer.PlaySound(SoundTypes.PICKUP);
                Destroy(gameObject);
            }


        }
    }
}
