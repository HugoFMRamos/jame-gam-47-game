using UnityEngine;

public class GameOverPlane : MonoBehaviour
{
    public enum GameOverType
    {
        Fall,
        Egg,
        Crow
    }

    public GameOverType type;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StackingCube") && other.gameObject.GetComponent<StackingCube>().isActive)
        {
            if (type == GameOverType.Fall) GameManager.Instance.GameOver(0);
            else if (type == GameOverType.Egg) GameManager.Instance.GameOver(1);
            else GameManager.Instance.GameOver(2);
        }
    }
}
