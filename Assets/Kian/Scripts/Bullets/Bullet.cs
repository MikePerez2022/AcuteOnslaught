using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public bool inUse = false;
    public bool usedPreviously = false;
    public float lifespan = 5.0f;

    private float currentLifespan;

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private void Update()
    {
        currentLifespan -= Time.deltaTime;

        if (currentLifespan <= 0 && inUse)
        {
            Debug.Log("Lifespan");
            EndUsage();
        }
    }

    public virtual void SetInUse()
    {
        inUse = true;
        usedPreviously = true;
        currentLifespan = lifespan;

        gameObject.SetActive(true);

        rb.linearVelocity = transform.up * speed;

    }

    public virtual void EndUsage()
    {
        inUse = false;
        currentLifespan = 0;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            EndUsage();
        }
        Debug.Log("Trigger");
    }
}
