using UnityEngine;

public class Water : MonoBehaviour
{
    GlobalInfo global;
	Rigidbody2D rb;

	void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		rb = collision.gameObject.GetComponent<Rigidbody2D>();
		if (collision.CompareTag("Player")) global.inWater = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) global.inWater = false;
		else if (rb) rb = null;
	}
}
