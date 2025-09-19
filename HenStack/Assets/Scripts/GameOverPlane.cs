using UnityEngine;

public class GameOverPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("StackingCube") && other.gameObject.GetComponent<StackingCube>().isActive) {
            GameManager.Instance.GameOver();
        }
    }
}
