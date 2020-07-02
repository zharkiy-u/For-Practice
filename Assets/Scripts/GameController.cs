using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GlobalInfo global;
    private GameObject player;

    void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (player.gameObject.GetComponent<DistanceJoint2D>().connectedBody)
            {
                player.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
                player.gameObject.GetComponent<DistanceJoint2D>().connectedBody = null;
                if(global.active_hook) Destroy(global.active_hook);
            }
            player.transform.position = player.GetComponent<RespawnPoint>().spawn_point;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu_levels");
        }

        SkillChanger();
    }

    void SkillChanger()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) global.skill_number = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) global.skill_number = 2;
    }
}
