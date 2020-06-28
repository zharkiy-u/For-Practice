using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Vector2 spawn_point;

    void Start()
    {
        spawn_point = gameObject.transform.position;
    }
}
