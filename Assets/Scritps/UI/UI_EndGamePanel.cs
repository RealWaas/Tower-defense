using UnityEngine;

public class UI_EndGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    private void Awake()
    {
        GameManager.OnVictory += OpenVictoryPanel;
        GameManager.OnDefeat += OpenDefeatPanel;

        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.OnVictory -= OpenVictoryPanel;
        GameManager.OnDefeat -= OpenDefeatPanel;
    }

    private void OpenVictoryPanel()
    {
        victoryPanel.SetActive(true);
    }

    private void OpenDefeatPanel()
    {
        defeatPanel.SetActive(true);
    }
}
