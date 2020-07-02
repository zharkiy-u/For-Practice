using UnityEngine;
using UnityEngine.UI;

public class GUI_Functions : MonoBehaviour
{
    GlobalInfo global;
    public Text skill_name;
    public Slider skill_power;
    private GameObject gui_f;

    public bool isButton = false;
    public bool skills_on = true;

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        gui_f = GameObject.Find("ShowF");
    }

    void Update()
    {
		if (skills_on)
		{
            switch (global.skill_number)
            {
                case 1:
                    skill_name.text = "Telekinesis";
                    skill_power.gameObject.SetActive(true);
                    skill_power.value = global.power_value;
                    break;
                case 2:
                    skill_name.text = "Hook";
                    skill_power.gameObject.SetActive(false);
                    break;
            }
        }
        ShowF();
    }

    private void ShowF()
	{
		if (isButton) gui_f.SetActive(true);
		else gui_f.SetActive(false);
	}
}
