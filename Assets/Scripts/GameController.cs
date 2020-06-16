using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    GlobalInfo global;
    public GameObject hook_starter;
    public GameObject player;
    public Light2D global_light;

    private LayerMask mask = 1 << 11; //only hookable
    private LayerMask exeptions = (1 << 13); //purple

    void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }

    void Update()
    {
        //ScreenScale();

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (global_light.intensity == 0) global_light.intensity = 0.8f;
            else global_light.intensity = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
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
                    RaycastHit2D hit = Physics2D.Raycast(player.transform.position, global.mouse_position - (Vector2)player.transform.position, 6f, mask);
                    RaycastHit2D exeption = Physics2D.Raycast(player.transform.position, global.mouse_position - (Vector2)player.transform.position, 6f, exeptions);
                    if (hit && !global.active_hook && (!exeption || hit.distance < exeption.distance))
					{
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

    /*void ScreenScale()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1 && global.camera_scale > 2f) global.camera_scale--;
        else
        if (mw < -0.1 && global.camera_scale < 25f) global.camera_scale++;
    }*/
}
