using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject waveButton;

    private void Awake()
    {
        GameManager.OnInitGame += InitGame;
        GameManager.OnWaveStart += WaveStart;
        GameManager.OnWaveEnd += WaveEnd;

        ShowMainMenu();
    }

    private void OnDestroy()
    {
        GameManager.OnInitGame -= InitGame;
        GameManager.OnWaveStart -= WaveStart;
        GameManager.OnWaveEnd -= WaveEnd;
    }

    private void HideAllUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        waveButton.SetActive(false);
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

    private void WaveEnd()
    {
        HideAllUI();
        gameUI.SetActive(true);
        waveButton.SetActive(true);
    }

}
