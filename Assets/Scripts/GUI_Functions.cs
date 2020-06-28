using UnityEngine;
using UnityEngine.UI;

public class GUI_Functions : MonoBehaviour
{
    GlobalInfo global;
    public Text skill_name;
    public Slider skill_power;

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }

    void Update()
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
}
