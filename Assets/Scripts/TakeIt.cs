using System.Linq;
using UnityEngine;

public class TakeIt : MonoBehaviour
{
    bool isTaken = false;
    Rigidbody2D rb;
    GlobalInfo global;
    public Vector2 startPos;

    private LayerMask mask = (1 << 8) | (1 << 11) | (1 << 12) | (1 << 13); //player, hookable, purple, blue layers

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        rb = GetComponent<Rigidbody2D>();
    }

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
            startPos = transform.position;
		}
	}

	void FixedUpdate()
    {
        if (global.power_value > 0 && !global.inNoPowerZone)
		{
            if (isTaken == true && global.skill_number == 1 && CheckVisibility(0))
            {
                if (Vector2.Distance(global.mouse_position, rb.position) > global.maxDistance)
                {
                    isTaken = false;
                    return;
                }
                Vector2 vector = global.mouse_position - rb.position;
                rb.AddForce(vector * global.power);
                global.power_value = (4f - Vector2.Distance(startPos, transform.position)) / 4f;
            }

            if (Input.GetMouseButton(1) && global.skill_number == 1 && CheckVisibility(1))
            {
                if (Vector2.Distance(global.mouse_position, rb.position) < 10f)
                {
                    Vector2 vector = global.mouse_position - rb.position;
                    rb.AddForce(vector * global.power);
                    global.power_value -= 0.04f;
                }
            }
        }
    }

    private bool CheckVisibility(int mouse_button)
	{
		switch (mouse_button)
		{
            case 0:
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, global.player_position - (Vector2)transform.position, 10f, mask);
                if (hit != null && hit[1].collider.CompareTag("Player")) return true;
                return false;
            case 1:
                RaycastHit2D hitToPlayer = Physics2D.Raycast(global.mouse_position, global.player_position - global.mouse_position, 10f, mask);
                RaycastHit2D hitToObject = Physics2D.Raycast(global.mouse_position, (Vector2)transform.position - global.mouse_position, 10f, mask);
                if (!hitToPlayer || !hitToObject) return false;
                if (hitToPlayer.collider.CompareTag("Player") && hitToObject.collider.gameObject == gameObject) return true;
                return false;
            default:
                return false;
		}
	}

    private void OnMouseDown()
    {
        isTaken = true;
    }

    private void OnMouseUp()
    {
        isTaken = false;
    }
}
