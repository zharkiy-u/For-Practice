using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
	[Range(2, 9)]
	public int next_level_number;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if(next_level_number < 9) ChangeLevel();
			else SceneManager.LoadScene("Menu_levels");
		}
	}

	private void ChangeLevel()
	{
		SceneManager.LoadScene("Level " + next_level_number.ToString());
	}
}
