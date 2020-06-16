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
        //if (collision.CompareTag("Platform")) isWall = false;
        if (collision.gameObject.layer == 13) isWall = false; // 13 == Blue
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13 && !global.inWater) isWall = true;
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && !global.inWater) isWall = true;
    }*/
}
