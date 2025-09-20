using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public bool hasCubeLanded;
    public int score = 0;
    public float moveSpeed = 2f;

    [Header("Spawning")]
    public GameObject[] cubePrefabs;
    private int balance = 0;

    [Header("Crane Settings")]
    public float orbitSpeedIncrement = 0.1f;
    public float orbitRadiusIncrement = 0.1f;
    public float maxOrbitSpeed = 3.5f;

    [Header("Bird Settings")]
    public GameObject bird;

    [Header("References")]
    public Transform cubeSpawnerTransform;
    public Transform worldTransform;
    public CircularMovement orbit;
    private GameObject nextCube;
    private GameObject prefabToSpawn;
    private AudioSource _as;

    [Header("Audio")]
    public AudioClip[] clips;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        Time.timeScale = 1f;
        _as = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hasCubeLanded)
        {
            MoveWorld();
        }

        if (score == 30)
        {
            bird.SetActive(true);
        }
    }

    public void IncrementScore()
    {
        score++;
        CanvasController.Instance.SetScoreText(score);
        if (score % 10 == 0 && score > 0 && orbit.speed < maxOrbitSpeed)
        {
            _as.PlayOneShot(clips[3], 1.0f);
            orbit.radius += orbitRadiusIncrement;
            orbit.speed += orbitSpeedIncrement;
        }
    }

    public void GameOver(int clipToPlay)
    {
        Time.timeScale = 0f;
        _as.PlayOneShot(clips[clipToPlay], 1.0f);
        CanvasController.Instance.GameOver(score);
    }

    public void UpdateBalance(CubeData cube)
    {
        if (cube.type == CubeData.CubeType.LeftEgg) balance -= 1;
        else if (cube.type == CubeData.CubeType.LeftEgg) balance += 1;

        balance = Mathf.Clamp(balance, -3, 3);
    }

    public void DeployNextCube()
    {
        hasCubeLanded = true;

        if (score < 10)
        {
            prefabToSpawn = cubePrefabs[0];
        }
        else if (score >= 10 && score < 20)
        {
            int i = Random.Range(0, 2);
            prefabToSpawn = cubePrefabs[i];
        }
        else
        {
            prefabToSpawn = PickBalancedPrefab();
        }

        nextCube = Instantiate(prefabToSpawn, cubeSpawnerTransform.position, Quaternion.identity);
        nextCube.transform.SetParent(cubeSpawnerTransform, true);
    }

    public GameObject PickBalancedPrefab()
    {
        List<GameObject> candidates = cubePrefabs.ToList();

        if (balance <= -2)
        {
            candidates.RemoveAll(p => p.GetComponent<CubeData>().type == CubeData.CubeType.LeftEgg);
        }
        else if (balance >= 2)
        {
            candidates.RemoveAll(p => p.GetComponent<CubeData>().type == CubeData.CubeType.RightEgg);
        }

        return candidates[Random.Range(0, candidates.Count)];
    }

    public StackingCube GetNextCube()
    {
        return nextCube.GetComponent<StackingCube>();
    }

    public void AddToWorld(GameObject gameObject)
    {
        gameObject.transform.SetParent(worldTransform, true);

        CubeData data = gameObject.GetComponent<CubeData>();
        if (data != null)
        {
            UpdateBalance(data);
        }
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

        if (child != null)
        {
            child.position = endPos;
        }
    }
}