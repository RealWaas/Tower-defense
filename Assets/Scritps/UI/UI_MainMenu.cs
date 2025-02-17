using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject quitMenu;

    public void StartGame() => GameManager.InitGame();

    public void OnPlayClicked()
    {
        if (playMenu.activeSelf == true)
            DisablePlayMenu();
        else
        {
            quitMenu.SetActive(false);
            playMenu.SetActive(true);
        }
    }
    public void QuitConfirm()
    {
        quitMenu.SetActive(true);
        playMenu.SetActive(false);
    }

    public void DisablePlayMenu() => playMenu.SetActive(false);
    public void DisableQuitMenu() => quitMenu.SetActive(false);


    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
				    Application.Quit();
    #endif
    }
}
