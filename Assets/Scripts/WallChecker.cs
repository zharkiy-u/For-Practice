using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public bool isWall = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13) isWall = true; // 13 == Blue
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13) isWall = false;
    }
}
