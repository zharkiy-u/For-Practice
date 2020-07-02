using UnityEngine;

public class Respawn : MonoBehaviour
{
	GlobalInfo global;
	DistanceJoint2D player_joint;

	private void Start()
	{
		global = GameObject.Find("info").GetComponent<GlobalInfo>();
		player_joint = GameObject.Find("Player").GetComponent<DistanceJoint2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//if (global.active_hook && (collision.CompareTag("Player") || CheckHeldItem(collision)))
		if (player_joint.connectedBody && (collision.CompareTag("Player") || CheckHeldItem(collision)))
		{
			player_joint.enabled = false;
			player_joint.connectedBody = null;
			if (global.active_hook) Destroy(global.active_hook);
		}

		if (collision.CompareTag("Player")) Play_Death_Sound();

		collision.gameObject.transform.position = collision.gameObject.GetComponent<RespawnPoint>().spawn_point;
		collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	private bool CheckHeldItem(Collider2D collision)
	{
		GameObject held_item = GameObject.Find("HookStarter(Clone)").GetComponent<HingeJoint2D>().connectedBody.gameObject;
		if (collision.gameObject.Equals(held_item)) return true;
		else return false;
	}
	
	private void Play_Death_Sound()
	{
		GameObject.Find("Soundtrack").GetComponent<Soundtrack>().event_number = 1;
	}
}
