using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Transform center;
    public SpriteRenderer birdSprite;
    public float radius = 5f;
    public float speed = 2f;
    public float _angle = 0f;

    void Update()
    {
        _angle += speed * Time.deltaTime;

        float x = Mathf.Cos(_angle) * radius;

        if (gameObject.transform.position.x > 10f)
        {
            birdSprite.flipX = true;
        }
        else if (gameObject.transform.position.x < -10f)
        {
            birdSprite.flipX = false;
        }

        transform.position = new Vector3(center.position.x + x, center.position.y, center.position.z);
    }
}