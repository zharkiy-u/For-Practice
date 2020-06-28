using UnityEngine;

public class Respawn : MonoBehaviour
{
	GlobalInfo global;
	GameObject player;

	private void Start()
	{
		global = GameObject.Find("info").GetComponent<GlobalInfo>();
		player = GameObject.Find("Player");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (global.active_hook && (collision.CompareTag("Player") || CheckHeldItem(collision)))
		{
			player.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
			player.gameObject.GetComponent<DistanceJoint2D>().connectedBody = null;
			Destroy(global.active_hook);
		}

		collision.gameObject.transform.position = collision.gameObject.GetComponent<RespawnPoint>().spawn_point;
		collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	private bool CheckHeldItem(Collider2D collision)
	{
		GameObject held_item = GameObject.Find("HookStarter(Clone)").GetComponent<HingeJoint2D>().connectedBody.gameObject;
		if (collision.gameObject.Equals(held_item)) return true;
		else return false;
	}
}
