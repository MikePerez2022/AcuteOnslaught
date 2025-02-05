using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public bool inUse = false;
    public bool usedPreviously = false;
    public float lifespan = 5.0f;

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public virtual void SetInUse()
    {
        inUse = true;
        usedPreviously = true;

        gameObject.SetActive(true);

        rb.linearVelocity = transform.up * speed;
    }

    public virtual void EndUsage()
    {
        inUse = false;
        gameObject.SetActive(false);
    }


}
