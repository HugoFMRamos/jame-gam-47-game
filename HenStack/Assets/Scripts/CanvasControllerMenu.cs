using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControllerMenu : MonoBehaviour
{
    public static CanvasControllerMenu Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
}
