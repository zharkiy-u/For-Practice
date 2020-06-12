using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    public Camera cam;
    public float effect;

    void Start()
    {
        startPos = transform.position.x;
    }
    
    void Update()
    {
        float distance = (cam.transform.position.x - startPos) * effect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
