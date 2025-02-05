using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    private Vector2 moveDir;
    private Vector2 aimDir;

    private bool shooting;

    private Vector2 RightTouchStartPos;
    private Vector2 RightTouchCurrentPos;
    private Vector2 LeftTouchStartPos;
    private float leftTouchDist;
    [SerializeField] private float leftMaxDragDist;


    private bool[] touchOnRight = new bool[2];
    //private PlayerInput input;
    //private InputAction T0PositionAction;
    //private InputAction T1PositionAction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //input = GetComponent<PlayerInput>();
        //T0PositionAction = input.actions["Touch 0"];
        //T1PositionAction = input.actions["Touch 1"];


    }

    private void Update()
    {
        int index = 0;
        foreach(Touch touch in Input.touches)
        {
            if (index <= 1)
            { 
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= Screen.width / 2)
                    {
                        LeftTouchStartPos = touch.position;
                        touchOnRight[index] = false;
                    }
                    else
                    {
                        RightTouchStartPos = touch.position;
                        touchOnRight[index] = true;
                        shooting = true;
                    }
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    if (touchOnRight[index] == false)
                    {
                        moveDir = (touch.position - LeftTouchStartPos).normalized;
                        leftTouchDist = Vector2.Distance(LeftTouchStartPos, touch.position);
                    }
                    else
                    {
                        aimDir = (touch.position - RightTouchStartPos).normalized;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (touchOnRight[index] == false)
                    {
                        moveDir = Vector2.zero;
                    }
                    else
                    {
                        shooting = false;
                    }
                }
            }
            index++;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = (moveDir.x * moveSpeed) * Mathf.Clamp01(leftTouchDist / leftMaxDragDist);
        rb.linearVelocityY = (moveDir.y * moveSpeed) * Mathf.Clamp01(leftTouchDist / leftMaxDragDist);
    }
    
}
