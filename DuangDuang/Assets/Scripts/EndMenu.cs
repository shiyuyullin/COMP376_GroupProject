using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void ButtonClickStr(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ButtonClickNum(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
