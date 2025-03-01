using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NextWaveButton : Button, IPointerDownHandler
{
    public override void OnPointerDown(PointerEventData eventData) => GameManager.WaveStart();
}
