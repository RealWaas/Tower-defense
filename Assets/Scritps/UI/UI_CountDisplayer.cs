using TMPro;
using UnityEngine;

public class UI_CountDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text countDisplayer;

    public void UpdateCount(int _count)
    {
        countDisplayer.text = _count.ToString();
    }
}
