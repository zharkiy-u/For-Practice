using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public bool isWall = false;
    GlobalInfo global;

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform")) isWall = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && !global.inWater) isWall = true;
    }
}
