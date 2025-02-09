using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public bool inUse = false;
    public bool usedPreviously = false;
    public float lifespan = 5.0f;

    private float currentLifespan;

    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject parent;

    private void Update()
    {
        currentLifespan -= Time.deltaTime;

        if (currentLifespan <= 0 && inUse)
        {
            Debug.Log("Lifespan");
            EndUsage();
        }
    }

    public virtual void SetInUse(GameObject parentObject)
    {
        inUse = true;
        usedPreviously = true;
        currentLifespan = lifespan;

        parent = parentObject;

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
