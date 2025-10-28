using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AptoPlantasScript : MonoBehaviour
{
    private void Start()
    {
        UpdateChildTexts();
    }

    private void UpdateChildTexts()
    {
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("PNLpl"))
            {
                string code = child.name.Substring(5); // Extrai o código após "PNLpl"

                
                foreach (Transform textApto in child)
                {
                    if (textApto.name.StartsWith("TitleApto"))
                    {
                        Debug.Log("Checkpoint 1");
                        TextMeshProUGUI textInput = textApto.GetComponent<TextMeshProUGUI>();
                        if (textInput != null)
                        {
                            Debug.Log("Checkpoint 2");
                            textInput.text = "Apartamento " + code;
                            
                        }
                    }
                }
            }
        }
    }
}