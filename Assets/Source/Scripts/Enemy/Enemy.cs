using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private Rigidbody2D _rigidbody;
    private bool _isHook;

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody2D>();
    }
    
    public void OnMouseDown()
    {
        _isHook = true;
        _playerAttack.Hook(this);
        _rigidbody.gravityScale = 0f;
        Hook();
    }

    private void Hook()
    {
        _rigidbody.velocity = (_playerAttack.transform.position - transform.position).normalized * 10f;
    }
}