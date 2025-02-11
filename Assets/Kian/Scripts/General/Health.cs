using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float health { get; private set; }

    public virtual void DealDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
