using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Juego");
    }
}
