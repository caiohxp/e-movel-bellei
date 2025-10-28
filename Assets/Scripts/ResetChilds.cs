using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetChilds : MonoBehaviour
{
    public bool status;
    public Button BTNreset;
    public GameObject PNLparent;
    public GameObject[] PNLexceptions;

    private void Start()
    {
        BTNreset.onClick.AddListener(ToggleChildren);
    }

    private void ToggleChildren()
    {
        // Desativa todos os filhos de PNLplanta
        foreach (Transform child in PNLparent.transform)
        {
            child.gameObject.SetActive(status);
        }

        foreach (GameObject pnl in PNLexceptions){
            pnl.SetActive(!status);
        }
    }
}
