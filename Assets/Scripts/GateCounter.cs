using TMPro;
using UnityEngine;

public class GateCounter : MonoBehaviour
{
    TextMeshPro counter;
    void Start()
    {
        counter = GameObject.Find("Text (TMP)").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<ButtonFunc>().isPlayer && Input.GetKeyDown(KeyCode.F))
		{
            counter.text = (int.Parse(counter.text) - 1).ToString();
            if (int.Parse(counter.text) == 0) Destroy(counter);
            Destroy(this);
		}
    }
}
