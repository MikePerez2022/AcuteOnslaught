using UnityEngine;

public class HealthPickup : Pickup
{
    public float healthToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                health.GainHealth(healthToGive);
                AudioPlayer.PlaySound(SoundTypes.PICKUP);
                Destroy(gameObject);
            }


        }
    }
}
