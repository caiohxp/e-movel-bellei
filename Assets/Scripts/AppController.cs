using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Se estiver rodando no editor, pare a reprodução
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Se estiver rodando como build, saia do aplicativo
            Application.Quit();
#endif
        }
    }

    public void ChangeScene(string lvlgame){
        SceneManager.LoadScene(lvlgame);
    }
}
