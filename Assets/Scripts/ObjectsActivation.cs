using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ObjectActivationController : MonoBehaviour
{
    public bool activeCameraMove;
    public bool isParent;
    public bool icon;
    public bool disableAutoNormalColor;
    public GameObject imgGloobyParent;
    public GameObject buttonsParent;
    public bool isDifferentCameraTarget;
    public GameObject[] cameraTargets;
    public GameObject[] imgGlobby; // Array para armazenar os objetos IMGGlobby
    public GameObject[] imgGloobySameBG; // Array para armazenar os objetos IMGGlobby com mesmo BG
    public Button[] buttons; // Array para armazenar os botões
    private CameraController cameraController;
    private bool alternativeTextColor;
    public Color activeColor;
    public Color inactiveColor;

    public Color activeTextColor;
    public Color inactiveTextColor;

    private void Start()
    {
        // Encontra a instância do CameraController na cena
        cameraController = FindObjectOfType<CameraController>();

        if (imgGloobyParent != null)
        {
            UpdateImgGlobby();
        }

        if (buttonsParent != null)
        {
            UpdateButtons();
        }

        // Adiciona a função OnButtonClick como listener para cada botão
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Captura o valor atual de i para o listener
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        if (!isDifferentCameraTarget)
        {
            cameraTargets = new GameObject[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                cameraTargets[i] = buttons[i].gameObject;
            }
        }

        UpdateButtonTexts();
    }

    void Update()
    {
        if (isParent)
        {
            alternativeTextColor = false;
            if (imgGloobySameBG != null)
            {
                foreach (var parent in imgGloobySameBG)
                {
                    if (parent.activeSelf)
                    {
                        alternativeTextColor = true;
                        break;
                    }
                }
            }
            for (int i = 0; i < imgGlobby.Length; i++)
            {
                if (imgGlobby[i].activeSelf)
                {
                    SetButtonColor(buttons[i], activeColor); // rgba(255,130,0,112)
                    SetTextColor(buttons[i], activeTextColor);
                    // if (!icon)
                    // SetChildImagesActive(buttons[i], true); // Passa a cor ativa
                }
                else
                {
                    SetButtonColor(buttons[i], inactiveColor); // Transparent
                    if(alternativeTextColor)
                        SetTextColor(buttons[i], activeColor);
                    else
                        SetTextColor(buttons[i], inactiveTextColor);
                    // SetChildImagesActive(buttons[i], false); // Passa a cor inativa
                }
            }
        }
    }

    private void UpdateButtonTexts()
    {
        foreach (Button button in buttons)
        {
            // Verifica se o nome começa com "BTN" e tem pelo menos 4 caracteres
            if (button.name.StartsWith("BTN") && button.name.Length > 3)
            {
                char fourthChar = button.name[3]; // Obtém o quarto caractere
                if (char.IsDigit(fourthChar)) // Verifica se o quarto caractere é um número
                {
                    string suffix = button.name.Substring(3); // Obtém o texto após "BTN"
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        buttonText.text = suffix; // Define o texto como o sufixo
                    }
                }
            }
        }
    }

    private void UpdateButtons()
    {
        buttons = buttonsParent.GetComponentsInChildren<Button>()
                               .Where(b => b.name.StartsWith("BTN"))
                               .ToArray();
    }

    private void UpdateImgGlobby()
    {
        if (imgGloobyParent != null)
        {
            var newImgGlobby = imgGloobyParent.GetComponentsInChildren<Image>(true)
                                          .Where(t => t.name.StartsWith("PNL"))
                                          .Select(t => t.gameObject)
                                          .ToArray();

            imgGlobby = imgGlobby.Concat(newImgGlobby).Distinct().ToArray();
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        // Desativa todos os objetos IMGGlobby
        foreach (GameObject img in imgGlobby)
        {
            img.SetActive(false);
        }

        // Ativa apenas o objeto IMGGlobby correspondente ao botão clicado
        imgGlobby[buttonIndex].SetActive(true);

        if (activeCameraMove && cameraController != null && cameraController.moveAble)
        {
            cameraController.CameraMove(cameraTargets[buttonIndex]);
        }
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        button.colors = cb;
    }

    private void SetTextColor(Button button, Color color)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null)
        {
            buttonText.color = color;
        }

        Image[] childImages = button.GetComponentsInChildren<Image>(true);
        foreach (Image img in childImages)
        {
            if (img.gameObject != button.gameObject)
            {
                img.color = color;
            }
        }
    }

    private void SetChildImagesActive(Button button, bool active)
    {
        foreach (Transform child in button.transform)
        {
            Image childImage = child.GetComponent<Image>();
            if (childImage != null)
            {
                childImage.gameObject.SetActive(active);
            }
        }
    }
}