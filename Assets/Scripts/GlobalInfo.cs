using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
    [Header("Positions")]
    public Vector2 mouse_position;
    public Vector2 player_position;

    [Header("Skills")]
    public float power;
    public float maxDistance;
    public int skill_number;
    public GameObject active_hook;
    public float power_value = 1f;

    [Header("Zones")]
    public bool inNoPowerZone = false;
    public bool inWater = false;

    void Start()
    {
        skill_number = 1;
    }

    void Update()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player_position = GameObject.Find("Player").transform.position;
    }
}
