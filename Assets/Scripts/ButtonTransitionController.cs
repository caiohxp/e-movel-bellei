using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public Image firstPart;
    public Image secPart;

    private bool isSelected = false;

    private void SetAlpha(float alpha)
    {
        Color upperColor = firstPart.color;
        upperColor.a = alpha;
        firstPart.color = upperColor;

        Color lowerColor = secPart.color;
        lowerColor.a = alpha;
        secPart.color = lowerColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            SetAlpha(0.44f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            SetAlpha(0f);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        SetAlpha(0.44f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        SetAlpha(0f);
    }
}
