using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<RespawnPoint>().spawn_point = collision.gameObject.transform.position;
			Destroy(gameObject);
		}
	}
}
