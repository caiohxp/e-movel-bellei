using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMenuButtons : MonoBehaviour
{
    public Button pnNatto;
    public Button btnFechar;
    public List<Button> buttons;

    private Color activeColor = new Color(77 / 255f, 93 / 255f, 85 / 255f, 188 / 255f);
    private Color inactiveColor = new Color(20 / 255f, 73 / 255f, 108 / 255f, 255 / 255f);

    void Start(){
        pnNatto.onClick.AddListener(() => SetButtonColors(activeColor));
        btnFechar.onClick.AddListener(() => SetButtonColors(inactiveColor));
    }

    void SetButtonColors(Color color)
    {
        foreach(Button b in buttons){
            b.image.color = color;
        }
    }
}
