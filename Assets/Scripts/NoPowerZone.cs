using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerZone : MonoBehaviour
{
    GlobalInfo global;

    void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
    }

	private void OnMouseEnter()
	{
        global.inNoPowerZone = true;
	}

	private void OnMouseExit()
	{
        global.inNoPowerZone = false;
    }
}
