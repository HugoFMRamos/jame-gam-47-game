using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public bool hasCubeLanded;
    public int score = 0;
    public float moveSpeed = 2f;

    [Header("CircularMovement")]
    public float orbitSpeedIncrement = 0.1f;
    public float orbitRadiusIncrement = 0.1f;
    public float maxOrbitSpeed = 3.5f;

    [Header("References")]
    public GameObject cubePrefab;
    public Transform cubeSpawnerTransform;
    public Transform worldTransform;
    public CircularMovement orbit;
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

    void Update()
    {
        if (hasCubeLanded)
        {
            MoveWorld();
        }
    }

    public void IncrementScore()
    {
        score++;
        if (score % 10 == 0 && score > 0 && orbit.speed < maxOrbitSpeed)
        {
            orbit.radius += orbitRadiusIncrement;
            orbit.speed += orbitSpeedIncrement;
        }
    }

    public void DeployNextCube()
    {
        hasCubeLanded = true;
        nextCube = Instantiate(cubePrefab, cubeSpawnerTransform.transform.position, Quaternion.identity);
        nextCube.transform.SetParent(cubeSpawnerTransform, true);
    }

    public StackingCube GetNextCube()
    {
        return nextCube.GetComponent<StackingCube>();
    }

    public void AddToWorld(GameObject gameObject)
    {
        gameObject.transform.SetParent(worldTransform, true);
    }

    public void MoveWorld()
    {
        foreach (Transform child in worldTransform)
        {
            StartCoroutine(MoveChildDown(child));
        }

        hasCubeLanded = false;
    }

    private IEnumerator MoveChildDown(Transform child)
    {
        if (child == null) yield break;

        Vector3 startPos = child.position;
        Vector3 endPos = new(startPos.x, Mathf.Round(startPos.y - 1f), startPos.z);
        float t = 0f;

        while (t < 1f)
        {
            if (child == null) yield break;
            t += Time.deltaTime * moveSpeed;
            child.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        child.position = endPos;
    }
}