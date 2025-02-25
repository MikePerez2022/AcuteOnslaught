using System.Collections;
using UnityEngine;

public class BreakingBullet : Bullet
{
    [SerializeField] private float timeBeforeBreak;
    [SerializeField] private Weapon breakStats;

    [SerializeField] private ShootingManagerSimple sm;



    public override void SetInUse(GameObject parentObject)
    {
        base.SetInUse(parentObject);

        StartCoroutine(BreakBullet());

    }

    private IEnumerator BreakBullet()
    {
        yield return new WaitForSeconds(timeBeforeBreak);

        StartCoroutine(sm.SpawnBullet(breakStats, -1));

        EndUsage();
    }
}
