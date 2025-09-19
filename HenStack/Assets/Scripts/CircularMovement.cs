using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public Transform center;
    public float radius = 5f;
    public float speed = 2f;
    private float _angle = 0f;

    void Update()
    {
        _angle += speed * Time.deltaTime;

        float x = Mathf.Cos(_angle) * radius;
        float y = Mathf.Sin(_angle) * radius / 2f;

        transform.position = new Vector3(center.position.x + x, center.position.y + y, center.position.z);
    }
}