using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StackingCube : MonoBehaviour
{
    public bool isActive = true;
    public float gravityScale = 5f;
    public LayerMask whatIsCube;
    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerInput _pi;

    void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_pi == null) _pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        _pi.OnMousePressed += DropCube;
    }

    void OnDisable()
    {
        _pi.OnMousePressed -= DropCube;
    }

    private void DropCube()
    {
        if (isActive)
        {
            gameObject.transform.SetParent(null, true);
            _rb.gravityScale = gravityScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Starter") || collision.gameObject.CompareTag("StackingCube")) && isActive)
        {
            isActive = false;
            GameManager.Instance.IncrementScore();
            GameManager.Instance.DeployNextCube();
            GameManager.Instance.AddToWorld(gameObject);

            _rb.velocity = Vector3.zero;
            _rb.gravityScale = 0f;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _ps.Play();
        }
    }
}