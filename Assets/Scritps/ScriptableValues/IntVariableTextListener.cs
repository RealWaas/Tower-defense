using TMPro;
using UnityEngine;

public class IntVariableTextListener : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;

    [SerializeField] private VariableObjectSO<int> variable;

    private void Awake()
    {
        variable.onValueChange += UpdateValue;
    }

    private void OnDestroy()
    {
        variable.onValueChange -= UpdateValue;
    }

    private void UpdateValue(int _value)
    {
        m_Text.text = _value.ToString();
    }
}
