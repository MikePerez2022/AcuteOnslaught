using UnityEngine;

public class LaserBullet : Bullet
{
    [SerializeField] LineRenderer lr;

    private bool disappear;
    [SerializeField] private float laserWidth;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float lerpSpeed;

    public override void SetInUse(GameObject parentObject)
    {
        base.SetInUse(parentObject);

        lr.widthMultiplier = laserWidth;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        disappear = true;

        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, lr.widthMultiplier * .5f, transform.up, 30);
        foreach(RaycastHit2D rayHit in hit)
        {
            if (rayHit.collider.gameObject != parentObject)
            {
                rayHit.collider.GetComponent<Health>().DealDamage(damage);
            }
        }
    }

    private void Update()
    {
        lr.SetPosition(1, Vector3.Lerp(lr.GetPosition(1), lr.GetPosition(0) + transform.up * 50, Time.deltaTime * lerpSpeed));

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
