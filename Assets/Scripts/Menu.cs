using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        // Play the main menu music
        MusicManager.Instance.PlayMusic("MainMenu");
    }
    public void StartBtn()
    {
        SceneManager.LoadScene("Game");
        MusicManager.Instance.PlayMusic("Game");
    }
}
