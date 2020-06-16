using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    GlobalInfo global;
	Rigidbody2D rb;
	float speed_limit = 3f;


	void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		rb = collision.gameObject.GetComponent<Rigidbody2D>();
		if (collision.CompareTag("Player")) global.inWater = true;
		//else if (rb) rb.gravityScale = 0.3f;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) global.inWater = false;

		else if (rb)
		{
			//rb.gravityScale = 1f;
			rb = null;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>())
		{
			if (rb.velocity.y < -speed_limit)	rb.velocity = new Vector2(rb.velocity.x, -speed_limit);
			if (rb.velocity.y > speed_limit)	rb.velocity = new Vector2(rb.velocity.x, speed_limit);
			if (rb.velocity.x < -speed_limit)	rb.velocity = new Vector2(-speed_limit, rb.velocity.y);
			if (rb.velocity.x > speed_limit)	rb.velocity = new Vector2(speed_limit, rb.velocity.y);
		}
	}
}
