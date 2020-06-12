using UnityEngine;

public class TakeIt : MonoBehaviour
{
    bool isTaken = false;
    Rigidbody2D rb;
    GlobalInfo global;
    public Vector2 startPos;

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
		if (global.power_value > 0)
		{
            if (isTaken == true && global.skill_number == 1)
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

            if (Input.GetMouseButton(1) && global.skill_number == 1)
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

    private void OnMouseDown()
    {
        isTaken = true;
    }

    private void OnMouseUp()
    {
        isTaken = false;
    }
}
