using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGround = false;
    GlobalInfo global;

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform") isGround = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Platform" && !global.inWater) isGround = true;
    }
}
