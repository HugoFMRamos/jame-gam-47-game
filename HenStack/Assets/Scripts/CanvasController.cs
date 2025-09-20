using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject gameScreen;
    public GameObject gameOverScreen;

    private string[] _names = { "Attila the hen!", "Audrey Henburn!", "Arrietty!", "Beyoncé!", "Baby Spice!",
                                "Billie Jean!", "Bob II!", "Buttercup!", "Cream Puff!", "Coco!", "Chilli!",
                                "Dolly Part-hen!", "Driving Miss Daisy!", "Drumstick!", "Dumpling!",
                                "Eggtor!", "Egg!", "Egbert!", "Floob!", "Fluffy Bum!", "Fajita!", "Granny Weatherwax!",
                                "Gwyneth Poultry!", "Goober!", "Goldie!", "Hob Nob!", "Hulk Yolkan!", "Henrique Egglecias!",
                                "India!", "Irene!", "Ivy!", "Jenny!", "Jade!", "Kentucky!", "Kiev!", "Lucky Clucker!",
                                "Luna!", "Little Miss!", "Lindy Hop!", "Mother Clucker!", "Marilyn Mon-rhode!", "Miss Pompom!",
                                "Mrs Peacock!", "Norma Jean!", "Nanny Ogg!", "Nacho!", "Omlette!", "Oprah!", "Princess Layer!",
                                "Penelope!", "Queen Eggzabeth, Goddess of Thy Eggs IV!", "Sofia Lor-hen!", "Snow Whites!",
                                "Sweetpea!", "Smasher!", "Scramble!", "Twilight!", "Terry!", "Tallulah!", "Uma!", "Ursula!",
                                "Vanessa!", "Vindaloo!", "Vicky!", "Whitney Eggston!", "Winnie The Egg!", "Wanda!", "Yolko Ono!",
                                "Yolkozuna!", "Yardley!", "Zakira!", "Zoey!"};

    private string[] _phrases = { "", "No! Not ", "Why did you do that to ", "Oh my god, ", "NO! My baby ", "Oh no, ",
                                "Dear god, not ", "No, no, no! ", "No! My precious "};

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        SetScoreText(0);
        gameScreen.SetActive(true);
        gameOverScreen.SetActive(false);

    }

    public void SetScoreText(int score)
    {
        scoreText.text = "SCORE: " + score;
    }

    public void GameOver(int score)
    {
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
        gameOverScoreText.text = "SCORE: " + score;
        GenerateLosingPhrase();
    }

    private void GenerateLosingPhrase()
    {
        int i = Random.Range(0, _names.Length);
        int j = Random.Range(0, _phrases.Length);

        nameText.text = _phrases[j] + _names[i];
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
