using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject quitMenu;
    public Button continueButton;

    private void OnEnable()
    {
        // Check if there is a file to be continued
        if(!DataManager.HasSaveFile())
            continueButton.interactable = false;
        else
            continueButton.interactable = true;
    }

    public void StartGame() => GameManager.InitGame();
    public void ContinueGame() => GameManager.LoadGame();

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

    public void QuitGame() => GameManager.QuitGame();
}
