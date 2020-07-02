using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Button_Levels()
	{
		SceneManager.LoadScene("Menu_levels");
	}
	public void Button_Controls()
	{
		SceneManager.LoadScene("Menu_controls");
	}
	public void Button_Exit()
	{
		Application.Quit();
	}

	public void Button_Menu()
	{
		SceneManager.LoadScene("Menu_main");
	}

	public void Level_1()
	{
		SceneManager.LoadScene("Level 1");
	}
	public void Level_2()
	{
		SceneManager.LoadScene("Level 2");
	}
	public void Level_3()
	{
		SceneManager.LoadScene("Level 3");
	}
	public void Level_4()
	{
		SceneManager.LoadScene("Level 4");
	}
	public void Level_5()
	{
		SceneManager.LoadScene("Level 5");
	}
	public void Level_6()
	{
		SceneManager.LoadScene("Level 6");
	}
	public void Level_7()
	{
		SceneManager.LoadScene("Level 7");
	}
	public void Level_8()
	{
		SceneManager.LoadScene("Level 8");
	}
}
