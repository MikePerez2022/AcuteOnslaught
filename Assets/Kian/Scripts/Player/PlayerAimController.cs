using UnityEngine;
using UnityEngine.UI;

public class PlayerAimController : MonoBehaviour
{
    public Vector2 aimDir;

    private int touchIndex;

    private Vector2 rightTouchStartPos;
    [SerializeField] private float rightMinTouchDist;
    [SerializeField] private float rightMaxTouchDist;
    private bool shooting;

    [SerializeField] private ShootingManager sm;
    [SerializeField] private Image rightJoystick;

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
                    rightJoystick.gameObject.SetActive(true);
                    rightJoystick.transform.position = touch.position;
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

                    if (Vector2.Distance(rightJoystick.transform.position, rightTouchStartPos) >= rightMaxTouchDist * .95f)
                    {
                        rightJoystick.transform.position = rightTouchStartPos + ((touch.position - rightTouchStartPos).normalized * rightMaxTouchDist);
                    }
                    else
                    {
                        rightJoystick.transform.position = touch.position;
                    }
                }
                else if (touch.phase == TouchPhase.Ended && index == touchIndex)
                {
                    sm.Shoot(false);
                    touchIndex = -1;

                    rightJoystick.gameObject.SetActive(false);
                }
            }
            index++;
        }
    }
}
