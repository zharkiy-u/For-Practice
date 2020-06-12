using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Flashlight : MonoBehaviour
{
    GlobalInfo global;
    Light2D light2d;

    void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        light2d = GetComponent<Light2D>();
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.L))
		{
            if (light2d.enabled) light2d.enabled = false;
            else light2d.enabled = true;
		}

        Vector2 direction = global.mouse_position - (Vector2)transform.position;
        float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
