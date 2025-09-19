using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public GameObject cubePrefab;
    public Transform cubeSpawner;
    private GameObject nextCube;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void DeployNextCube()
    {
        nextCube = Instantiate(cubePrefab, cubeSpawner.transform.position, Quaternion.identity);
    }

    public StackingCube GetNextCube()
    {
        return nextCube.GetComponent<StackingCube>();
    }
}