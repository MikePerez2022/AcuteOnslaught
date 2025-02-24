using UnityEngine;

public class LaserBullet : Bullet
{
    [SerializeField] LineRenderer lr;

    private bool disappear;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float lerpSpeed;

    [SerializeField] private LayerMask wallMask;
    [SerializeField] protected LayerMask hitMask;

    private Vector2 hitLocation;
    public override void SetInUse(GameObject parentObject)
    {
        base.SetInUse(parentObject);

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        disappear = true;

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, wallMask);
        hitLocation = wallCheck.point;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, lr.widthMultiplier * .5f, transform.up, Vector2.Distance(transform.position, wallCheck.point), hitMask);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent(out Health health))
                {
                    float actualDamage = (scaleEffectsDamage) ? damage * lr.widthMultiplier : damage;
                    health.DealDamage(actualDamage);
                };
            }
        }

        
    }

    public override void SetStats(Weapon weapon)
    {
        base.SetStats(weapon);

        lr.widthMultiplier = weapon.Scale;

    }

    private void Update()
    {
        lr.SetPosition(1, Vector3.Lerp(lr.GetPosition(1), hitLocation, Time.deltaTime * lerpSpeed));

        if (disappear)
        {
            lr.widthMultiplier = Mathf.Lerp(lr.widthMultiplier, 0, Time.deltaTime * shrinkSpeed);

            if (lr.widthMultiplier <= 0.001f)
            {
                EndUsage();
            }
        }
    }
}
