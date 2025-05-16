using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // stater spillet fra første scene
    public void Play()
    {
        SceneManager.LoadScene("...");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
