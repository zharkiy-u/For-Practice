using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    [Header("Source")]
    public AudioSource event_sound;

    [Header("Clips")]
    public AudioClip death_sound;
    public AudioClip switch_sound;

    public float event_number = 0; // 0 - nothing

    public static Soundtrack instance = null;

	private void Start()
	{
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	void Update()
    {
        if(event_number != 0)
		{
			switch (event_number)
			{
                case 1:
                    event_sound.PlayOneShot(death_sound);
                    break;
                case 2:
                    event_sound.PlayOneShot(switch_sound);
                    break;
            }
            event_number = 0;
		}
    }
}
