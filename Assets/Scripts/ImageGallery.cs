using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGallery : MonoBehaviour
{
    public Image[] images; // Arraste todas as suas imagens da galeria para este array

    public Button buttonNext;
    public Button buttonPrev;

    // Variável para rastrear o índice da imagem atualmente visível
    private int currentIndex = 0;

    // O método Start é chamado uma vez quando o script é habilitado, antes de qualquer frame
    void Start()
    {
        // Garante que os botões não causem erro se não forem atribuídos
        if (buttonNext == null || buttonPrev == null)
        {
            Debug.LogError("Os botões de navegação não foram atribuídos no Inspector!");
            return;
        }

        // Garante que há imagens na galeria para exibir
        if (images == null || images.Length == 0)
        {
            Debug.LogError("Nenhuma imagem foi atribuída ao array 'images' no Inspector!");
            // Desativa os botões se não houver imagens
            buttonNext.interactable = false;
            buttonPrev.interactable = false;
            return;
        }

        // Adiciona 'listeners' aos botões. Isso faz com que os métodos ShowNextImage e ShowPreviousImage
        // sejam chamados sempre que os respectivos botões forem clicados.
        buttonNext.onClick.AddListener(ShowNextImage);
        buttonPrev.onClick.AddListener(ShowPreviousImage);

        // Inicia a galeria exibindo apenas a primeira imagem
        ShowImageAtIndex(0);
    }

    /// <summary>
    /// Avança para a próxima imagem na galeria.
    /// </summary>
    public void ShowNextImage()
    {
        // Incrementa o índice
        currentIndex++;

        // Se o índice passar do final do array, ele volta para o início (0)
        if (currentIndex >= images.Length)
        {
            currentIndex = 0;
        }

        ShowImageAtIndex(currentIndex);
    }

    /// <summary>
    /// Retorna para a imagem anterior na galeria.
    /// </summary>
    public void ShowPreviousImage()
    {
        // Decrementa o índice
        currentIndex--;

        // Se o índice ficar menor que 0, ele vai para o final do array
        if (currentIndex < 0)
        {
            currentIndex = images.Length - 1;
        }

        ShowImageAtIndex(currentIndex);
    }

    private void ShowImageAtIndex(int index)
    {
        // Passa por todas as imagens no array
        for (int i = 0; i < images.Length; i++)
        {
            // Ativa o GameObject da imagem se o índice dela for o que queremos mostrar,
            // e desativa se não for.
            images[i].gameObject.SetActive(i == index);
        }
    }
}