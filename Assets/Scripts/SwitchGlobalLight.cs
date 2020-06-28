using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class SwitchGlobalLight : MonoBehaviour
{
	private Light2D global_light;

	private void Start()
	{
		global_light = GameObject.Find("Global Light 2D").GetComponent<Light2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) global_light.intensity = 0f;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) global_light.intensity = 0.8f;
	}
}
