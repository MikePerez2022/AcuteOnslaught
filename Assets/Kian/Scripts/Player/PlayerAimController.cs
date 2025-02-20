using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public Vector2 aimDir;

    private int touchIndex;

    private Vector2 rightTouchStartPos;
    [SerializeField] private float rightMinTouchDist;
    private bool shooting;

    [SerializeField] private ShootingManager sm;

    private void Start()
    {
        touchIndex = -1;
    }

    private void Update()
    {
        int index = 0;
        foreach (Touch touch in Input.touches)
        {
            if (index <= 1)
            {
                if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                {
                    touchIndex = index;
                    rightTouchStartPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved && index == touchIndex)
                {
                    if (Vector2.Distance(touch.position, rightTouchStartPos) > rightMinTouchDist)
                    {
                        sm.Shoot(true);
                    }
                    aimDir = (touch.position - rightTouchStartPos).normalized;
                    float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

                    transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                }
                else if (touch.phase == TouchPhase.Ended && index == touchIndex)
                {
                    sm.Shoot(false);
                    touchIndex = -1;
                }
            }
            index++;
        }
    }
}
