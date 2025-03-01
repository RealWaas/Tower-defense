using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject preparationUI;
    [SerializeField] private GameObject ItemChoiceUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button saveButton;

    private void Awake()
    {
        GameManager.OnInitGame += InitGame;
        GameManager.OnWaveStart += WaveStart;

        PreparationManager.OnPreparation += Preparation;
        PreparationManager.OnItemChoice += ItemChoice;

        ShowMainMenu();
    }

    private void OnDestroy()
    {
        GameManager.OnInitGame -= InitGame;
        GameManager.OnWaveStart -= WaveStart;

        PreparationManager.OnPreparation -= Preparation;
        PreparationManager.OnItemChoice -= ItemChoice;
    }

    private void HideAllUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        preparationUI.SetActive(false);
        pauseMenu.SetActive(false);
        ItemChoiceUI.SetActive(false);
    }

    private void ShowMainMenu()
    {
        HideAllUI();
        mainMenu.SetActive(true);
    }

    private void InitGame()
    {
        HideAllUI();
        gameUI.SetActive(true);
    }

    private void WaveStart()
    {
        HideAllUI();
        gameUI.SetActive(true);
    }

    private void Preparation()
    {
        HideAllUI();
        gameUI.SetActive(true);
        preparationUI.SetActive(true);
    }
    private void ItemChoice()
    {
        HideAllUI();
        ItemChoiceUI.SetActive(true);
    }

    public void PauseGame()
    {
        HideAllUI();
        GameManager.PauseTime();
        saveButton.interactable = !GameManager.isInWave; // Only outside of waves
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        HideAllUI();
        GameManager.ResumeTime();
        if(!GameManager.isInWave)
            Preparation();
        else 
            WaveStart();
    }

    public void SaveAndQuit()
    {
        // TODO
        // Quit to main menu
        DataManager.SaveGame();
        GameManager.QuitGame();
    }
}
