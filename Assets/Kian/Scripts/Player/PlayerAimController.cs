using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public Vector2 aimDir;

    private int touchIndex;

    private Vector2 RightTouchStartPos;
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
                    RightTouchStartPos = touch.position;
                    sm.Shoot(true);
                }
                else if (touch.phase == TouchPhase.Moved && index == touchIndex)
                {
                    aimDir = (touch.position - RightTouchStartPos).normalized;
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
