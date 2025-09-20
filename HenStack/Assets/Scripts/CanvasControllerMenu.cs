using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControllerMenu : MonoBehaviour
{
    public static CanvasControllerMenu Instance { get; private set; }
    public GameObject menuScreen;
    public GameObject h2pscreen;
    private bool onH2PScreen = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        menuScreen.SetActive(true);
        h2pscreen.SetActive(false);

    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void HowToPlay()
    {
        onH2PScreen = !onH2PScreen;
        menuScreen.SetActive(!onH2PScreen);
        h2pscreen.SetActive(onH2PScreen);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
