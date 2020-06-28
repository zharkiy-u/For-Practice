using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GlobalInfo global;

    private GroundChecker ground_checker;
    private WallChecker left, right;

    private Rigidbody2D rb;
    public float speed;
    public float jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        ground_checker = GameObject.Find("GroundChecker").GetComponent<GroundChecker>();
        left = GameObject.Find("Left").GetComponent<WallChecker>();
        right = GameObject.Find("Right").GetComponent<WallChecker>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (global.active_hook)
            {
                GetComponent<DistanceJoint2D>().enabled = false;
                GetComponent<DistanceJoint2D>().connectedBody = null;
                Destroy(global.active_hook);
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
    }

    void FixedUpdate()
    {
        float speed_limit;
        if (!global.inWater) speed_limit = 7f;
        else speed_limit = 4f;

        if (rb.velocity.y < -speed_limit * 2) rb.velocity = new Vector2(rb.velocity.x, -speed_limit * 2); //fall speed limit
        if (global.inWater && rb.velocity.y > speed_limit) rb.velocity = new Vector2(rb.velocity.x, speed_limit);

        if (Input.GetKey(KeyCode.A) && rb.velocity.x > -speed_limit)
        {
            rb.AddForce(new Vector2(-transform.right.x, 0) * speed);
        }

        if (Input.GetKey(KeyCode.D) && rb.velocity.x < speed_limit)
        {
            rb.AddForce(new Vector2(transform.right.x, 0) * speed);
        }

		if (Input.GetKey(KeyCode.Space) && global.inWater)
		{
            rb.velocity += Vector2.up * 0.7f;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ground_checker.isGround)
			{
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }
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
        }
        FixGravity();
    }

    private void FixGravity()
	{
        float gravity;
        if (ground_checker.isGround) gravity = 0;
        else gravity = Physics2D.gravity.y;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * gravity * 0.04f;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * gravity * 0.02f;
        }
    }
}
