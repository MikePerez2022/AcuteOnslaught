using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController instance;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    private Vector2 moveDir;

    private int touchIndex = -1;

    private Vector2 LeftTouchStartPos;
    private float leftTouchDist;
    [SerializeField] private float leftMaxDragDist;
    [SerializeField] private float leftMinDragDist;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        touchIndex = -1;
    }

    private void Update()
    {
        int index = 0;
        foreach(Touch touch in Input.touches)
        {
            if (index <= 1)
            { 
                if (touch.phase == TouchPhase.Began && touch.position.x <= Screen.width / 2)
                {
                    touchIndex = index;
                    LeftTouchStartPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved && index == touchIndex)
                {
                    moveDir = (touch.position - LeftTouchStartPos).normalized;
                    leftTouchDist = Vector2.Distance(LeftTouchStartPos, touch.position);
                }
                else if (touch.phase == TouchPhase.Ended && index == touchIndex)
                {
                    touchIndex = -1;
                    moveDir = Vector2.zero;
                    rb.linearVelocity = Vector2.zero;
                }
            }
            index++;
        }
    }

    private void FixedUpdate()
    {
        if (leftTouchDist > leftMinDragDist)
        {
            rb.linearVelocityX = (moveDir.x * moveSpeed) * Mathf.Clamp01((leftTouchDist - leftMinDragDist) / leftMaxDragDist);
            rb.linearVelocityY = (moveDir.y * moveSpeed) * Mathf.Clamp01((leftTouchDist - leftMinDragDist) / leftMaxDragDist);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
}
