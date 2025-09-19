using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public StackingCube cube;
    public event Action OnMousePressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMousePressed?.Invoke();
        }

        if (!cube.isActive)
        {
            cube = GameManager.Instance.GetNextCube();
        }
    }
}
