using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [HideInInspector] public bool inUse = false;
    [HideInInspector] public bool usedPreviously = false;
    public float lifespan = 5.0f;

    private float currentLifespan;

    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    protected GameObject parent;

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
        parent = parentObject;
        inUse = true;
        usedPreviously = true;
        currentLifespan = lifespan;

        rb.linearVelocity = Vector3.zero;
        rb.linearVelocity = transform.up * speed;

        gameObject.SetActive(true);


    }

    public virtual void EndUsage()
    {
        inUse = false;
        currentLifespan = 0;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
            EndUsage();
        }

        else if (collision.CompareTag("Arena"))
            EndUsage();
        Debug.Log("Trigger");
    }
}
