using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public GameObject hook_starter;
    private GlobalInfo global;
    private GameObject player;

    private LayerMask mask = 1 << 11; //only hookable
    private LayerMask exeptions = (1 << 13); //purple

    void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (global.active_hook)
            {
                player.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
                player.gameObject.GetComponent<DistanceJoint2D>().connectedBody = null;
                Destroy(global.active_hook);
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

		if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
		{
            switch (global.skill_number)
            {
                case 1:
                    if(global.power_value < 1) global.power_value += 0.01f;
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            switch (global.skill_number)
            {
                case 1:
                    break;
                case 2:
                    RaycastHit2D hit = Physics2D.Raycast(player.transform.position, global.mouse_position - (Vector2)player.transform.position, 10f, mask);
                    RaycastHit2D exeption = Physics2D.Raycast(player.transform.position, global.mouse_position - (Vector2)player.transform.position, 10f, exeptions);
                    if (hit && (!exeption || hit.distance < exeption.distance))
					{
                        if (global.active_hook)
                        {
                            player.GetComponent<DistanceJoint2D>().enabled = false;
                            player.GetComponent<DistanceJoint2D>().connectedBody = null;
                            Destroy(global.active_hook);
                        }

                        GameObject hook = Instantiate(hook_starter, hit.point, Quaternion.identity);
                        hook.GetComponent<HingeJoint2D>().connectedBody = hit.rigidbody;
                    }
                    break;
            }
        }
    }

    void SkillChanger()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) global.skill_number = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) global.skill_number = 2;
    }
}
