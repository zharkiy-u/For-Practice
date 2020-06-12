using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GlobalInfo global;

    private GroundChecker ground_cheker;
    private WallChecker left, right;

    private Rigidbody2D rb;
    public float speed;
    public float jump;


    //jump direction
    // -1 - left
    //  1 - right
    //  0 - none
    public int direction = 0;


    public bool keep = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        ground_cheker = GameObject.Find("GroundChecker").GetComponent<GroundChecker>();
        left = GameObject.Find("Left").GetComponent<WallChecker>();
        right = GameObject.Find("Right").GetComponent<WallChecker>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (keep)
            {
                GetComponent<DistanceJoint2D>().enabled = false;
                GetComponent<DistanceJoint2D>().connectedBody = null;
                keep = false;
                if (global.active_hook) Destroy(global.active_hook);
                return;
            }
            /*RaycastHit2D [] hit = Physics2D.RaycastAll(transform.position, global.mouse_position - (Vector2)transform.position, 1.5f);
            Debug.DrawLine(transform.position, global.mouse_position, Color.red);

            if (hit.Length > 1)
            {
                if (!keep)
                {
                    if (hit[1].collider.CompareTag("Rope"))
                    {
                        GetComponent<DistanceJoint2D>().enabled = true;
                        GetComponent<DistanceJoint2D>().connectedBody = hit[1].rigidbody;
                        keep = true;
                    }
                }
                
            }*/
        }
        if (ground_cheker.isGround) direction = 0;
    }

    void FixedUpdate()
    {
        float speed_limit = 7f;
        //if (!keep) speed_limit = 7f;
        //else speed_limit = 10f;

        if (rb.velocity.y < -speed_limit * 2) rb.velocity = new Vector2(rb.velocity.x, -speed_limit * 2); //fall speed limit

        if (Input.GetKey(KeyCode.A))
        {
            if (direction > 0 && !keep) rb.velocity = new Vector2(0f, rb.velocity.y);
            else if (rb.velocity.x > -speed_limit) rb.AddForce(new Vector2(-transform.right.x, 0) * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (direction < 0 && !keep) rb.velocity = new Vector2(0f, rb.velocity.y);
            else if (rb.velocity.x < speed_limit) rb.AddForce(new Vector2(transform.right.x, 0) * speed);
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (global.inWater) rb.velocity += Vector2.up * 0.7f;
            //else if (ground_cheker.isGround) rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            if (ground_cheker.isGround) rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            else
            {
                if (left.isWall)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(new Vector2(jump / 2, jump), ForceMode2D.Impulse);
                }
                if (right.isWall)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(new Vector2(-jump / 2, jump), ForceMode2D.Impulse);
                }
            }
            if (rb.velocity.x < 0) direction = -1;
            else if (rb.velocity.x > 0) direction = 1;
            //else direction = 0;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water")) global.inWater = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water")) global.inWater = false;
    }*/
}
