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

    protected bool scaleEffectsDamage;
    protected bool scalesOverTime;
    protected float scaleChangeOverTime;

    private void Update()
    {
        currentLifespan -= Time.deltaTime;

        if (scalesOverTime)
            transform.localScale += new Vector3(scaleChangeOverTime, scaleChangeOverTime);

        if ((currentLifespan <= 0 || transform.localScale.x <= 0) && inUse)
        {
            EndUsage();
        }
    }

    public virtual void SetInUse(GameObject parentObject)
    {
        gameObject.SetActive(true);
        parent = parentObject;
        inUse = true;
        usedPreviously = true;
        currentLifespan = lifespan;

        rb.linearVelocity = Vector3.zero;
        rb.linearVelocity = transform.up * speed;
    }

    public virtual void SetStats(Weapon weapon)
    {
        speed = weapon.Speed + Random.Range(weapon.RandomSpeedAddition.x, weapon.RandomSpeedAddition.y);
        float scaleChange = Random.Range(weapon.RandomScaleAddition.x, weapon.RandomScaleAddition.y);
        gameObject.transform.localScale = new Vector3(weapon.Scale + scaleChange, weapon.Scale + scaleChange, 1);
        damage = weapon.Damage;
        scaleEffectsDamage = weapon.ScaleEffectsDamage;
        scalesOverTime = weapon.ScalesOverTime;
        scaleChangeOverTime = weapon.ScaleChangeOverTime;
    }

    public virtual void EndUsage()
    {
        inUse = false;
        currentLifespan = 0;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health) && collision.gameObject != parent)
        {
            health.DealDamage((scaleEffectsDamage) ? damage * gameObject.transform.localScale.x : damage);
            EndUsage();
        }

        else if (collision.CompareTag("Arena"))
            EndUsage();
    }
}
