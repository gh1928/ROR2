using UnityEngine;

/// <summary> 
/// Responsible for moving the player automatically and 
/// reciving input. 
/// </summary> 
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary> 
    /// A reference to the Rigidbody component 
    /// </summary> 
    private Rigidbody rb;

    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5;

    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    public float swipeMove = 2f;

    public float minSwipeDistance = 0.25f;
    private float minSwipeDistancePixels;
    private Vector2 touchStart;

    private int primaryTouchID = -1;
    
    public float minScale = 0.5f;
    public float maxScale = 3f;
    private float currentScale = 1f;


    // Start is called before the first frame update
    void Start()
    {
        // Get access to our Rigidbody component 
        rb = GetComponent<Rigidbody>();

        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;
    }

    /// <summary>
    /// FixedUpdate is called at a fixed framerate and is a prime place to put
    /// Anything based on time.
    /// </summary>
    void FixedUpdate()
    {
        if(primaryTouchID == -1 && Input.touchCount > 0)
        {
            primaryTouchID = Input.touches[0].fingerId;
        }

        if(Input.touchCount == 0)
        {
            primaryTouchID = -1;
        }

        var h = Input.GetAxis("Horizontal");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            var pos = Camera.main.ScreenToViewportPoint(touch.position);
            if (pos.x < 0.5f)
            {
                h = -1f;
            }
            else
            {
                h = 1f;
            }
        }
        rb.AddForce(h * dodgeSpeed, 0, rollSpeed);

        foreach(var t in Input.touches)
        {
            if(t.fingerId == primaryTouchID)
            {
                SwipeTeleport(t);
            }
        }

        if (Input.touchCount > 0)
        {            
            SwipeTeleport(Input.touches[0]);
        }

        ScalePlayer();
    }

    private void SwipeTeleport(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:                
                touchStart = touch.position;                
                break;
            case TouchPhase.Moved:                
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:
                {                    
                    var touchEnd = touch.position;
                    var deltaX = touchEnd.x - touchStart.x;

                    if (Mathf.Abs(deltaX) > minSwipeDistancePixels)
                    {
                        var dir = (deltaX < 0) ? Vector3.left : Vector3.right;

                        RaycastHit hit;
                        if (!rb.SweepTest(dir, out hit, swipeMove))
                        {
                            var newPos = rb.position + dir * swipeMove;
                            rb.MovePosition(newPos);
                        }
                    }
                }
                break;
            case TouchPhase.Canceled:                
                break;
            default:
                break;
        }
    }
    private void ScalePlayer()
    {
        if (Input.touchCount != 2)
            return;

        var t0 = Input.touches[0];
        var t1 = Input.touches[1];

        var prevT0Pos = t0.position - t0.deltaPosition;
        var prevT1Pos = t1.position - t1.deltaPosition;

        var prevMag = Vector2.Distance(prevT1Pos, prevT0Pos);
        var currMag = Vector2.Distance(t0.position, t1.position);

        var delta = currMag - prevMag;
     
        currentScale = Mathf.Clamp(currentScale + delta, minScale, maxScale);
        transform.localScale = currentScale * Vector3.one; 
    }
}