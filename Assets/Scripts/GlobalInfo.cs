using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
    public float mouseX;
    public float mouseY;
    public Vector2 mouse_position;

    public float power;
    public float maxDistance;
    public int skill_number;
    public const int skill_count = 5;
    public GameObject active_hook;
    public float camera_scale = 10f;

    public float power_value = 1f;

    public bool inWater = false;
    // Start is called before the first frame update
    void Start()
    {
        skill_number = 1;
    }

    // Update is called once per frame
    void Update()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseX = mouse_position.x;
        mouseY = mouse_position.y;
    }
}
