using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject obj;
    private GlobalInfo global;
    public float speed = 6f;

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }

    void Update()
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 2f, -10);
        //float distance = Vector2.Distance(transform.position, obj.transform.position);
        //Vector3 targetPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 2f, -10f);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        GetComponent<Camera>().orthographicSize = global.camera_scale;
        //GetComponent<Camera>().orthographicSize = distance * 0.5f + 8f;
    }
}
