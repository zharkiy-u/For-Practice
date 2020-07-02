using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GlobalInfo global;

    private GroundChecker ground_checker;
    private WallChecker left, right;

    private Rigidbody2D rb;
    public float speed;
    public float jump;

    public GameObject hook_starter;

    private LayerMask mask = (1 << 11) | (1 << 13) | (1 << 16) | (1 << 17); //hookable, blue, wall, box layers

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
            if (global.active_hook || GetComponent<DistanceJoint2D>().isActiveAndEnabled)
            {
                GetComponent<DistanceJoint2D>().enabled = false;
                GetComponent<DistanceJoint2D>().connectedBody = null;
                if (global.active_hook) Destroy(global.active_hook);
            }
			else
			{
                LayerMask mask = 1 << 9; //rope layer
                RaycastHit2D hit = Physics2D.Raycast(transform.position, global.mouse_position - (Vector2)transform.position, 1.5f, mask);
                if (hit)
                {
                    GetComponent<DistanceJoint2D>().enabled = true;
                    GetComponent<DistanceJoint2D>().connectedBody = hit.rigidbody;
                }
            }
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            switch (global.skill_number)
            {
                case 1:
                    if (global.power_value < 1) global.power_value += 0.01f;
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            switch (global.skill_number)
            {
                case 1:
                    break;
                case 2:
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, global.mouse_position - (Vector2)transform.position, 10f, mask);
                    if (hit && hit.collider.gameObject.layer == 11)
                    {
                        if (global.active_hook)
                        {
                            GetComponent<DistanceJoint2D>().enabled = false;
                            GetComponent<DistanceJoint2D>().connectedBody = null;
                            Destroy(global.active_hook);
                        }

                        GameObject hook = Instantiate(hook_starter, hit.point, Quaternion.identity);
                        hook.GetComponent<HingeJoint2D>().connectedBody = hit.rigidbody;
                    }
                    break;
            }
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
