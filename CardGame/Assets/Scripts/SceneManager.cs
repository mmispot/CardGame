using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void Tutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

    public void Return()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    public void WinGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
    }

    public void LoseGame()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
    }
}
