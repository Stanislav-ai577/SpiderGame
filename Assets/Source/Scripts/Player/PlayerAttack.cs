using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Rigidbody2D _rigidbody;
    private PlayerInput _input;
    private bool _isAttack;

    private void OnValidate()
    {
        _lineRenderer ??= GetComponent<LineRenderer>();
        _rigidbody ??= GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _input.OnAttackKeyPressed += SwitchMod;
    }

    private void Start()
    {
        _rotateSpeed = 3f;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Camera.main != null)
            {
                RaycastHit2D raycast = Physics2D.Raycast(transform.position + new Vector3(0, 1), Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (raycast)
                {
                    _lineRenderer.SetPosition(0, transform.position);
                    _lineRenderer.SetPosition(1, raycast.point);
                }

                _rigidbody.gravityScale = 0f;
                _rigidbody.velocity = ((Vector3)raycast.point - transform.position).normalized * 10f;
            }
        }
        
        if (_isAttack)
            _view.Rotate(new Vector3(0, 0, _rotateSpeed));
        else
            _view.rotation = Quaternion.Euler(0,0,0);

        if (_enemy)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _enemy.transform.position);
        }
        else
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void Hook(Enemy enemy)
    {
        _enemy = enemy;
        SwitchMod();
    }
    
    private void SwitchMod()
    {
        _isAttack = !_isAttack;
        _view.gameObject.SetActive(_isAttack);
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (_isAttack)
                Destroy(enemy.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}