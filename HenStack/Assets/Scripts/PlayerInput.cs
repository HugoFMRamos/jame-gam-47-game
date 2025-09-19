using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public StackingCube cube;
    public event Action OnMousePressed;

    void Awake()
    {
        cube = GameObject.FindGameObjectWithTag("StackingCube").GetComponent<StackingCube>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.hasCubeLanded)
        {
            OnMousePressed?.Invoke();
        }

        if (!cube.isActive)
        {
            cube = GameManager.Instance.GetNextCube();
        }
    }
}
