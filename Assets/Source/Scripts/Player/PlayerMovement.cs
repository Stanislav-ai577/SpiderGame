using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private PlayerInput _input;
   [SerializeField] private Rigidbody2D _rigidbody;
   [SerializeField] private float _speed;
   [SerializeField] private float _jumpForce;
   [SerializeField] private bool _isCanJump;
   [SerializeField] private bool _isWall;
   private Wall _wall;

   private void OnValidate()
   {
      _rigidbody ??= GetComponent<Rigidbody2D>();
      _input ??= GetComponent<PlayerInput>();
      _input.OnChangeDirection += Move;
      _input.OnJumpKeyPressed += Jump;
   }

   private void Start()
   {
      _speed = 2f;
      _jumpForce = 5f;
      _isCanJump = true;
      _isWall = false;
   }

   private void Update()
   {
      if (_isWall)
         _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, 0, 0f));
   }

   private void Jump()
   {
      if (!_isCanJump && !_isWall)
         return;
      
      Vector2 jumpDirection = Vector2.up * _jumpForce;
      
      if (_wall)
         jumpDirection = new Vector2(transform.position.x - _wall.transform.position.x, _jumpForce);
      
      _rigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);
      _isCanJump = false;
   }
   
   private void Move(Vector2 direction)
   {
      _rigidbody.AddForce(direction * _speed, ForceMode2D.Force); 
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.TryGetComponent(out Ground ground))
         _isCanJump = true;
      
      if (other.gameObject.TryGetComponent(out Wall wall))
      {
         _wall = wall;
         _isCanJump = true;
         _isWall = true;
      }
   }
   private void OnCollisionExit2D(Collision2D other)
   {
      if (other.gameObject.TryGetComponent(out Wall ground))
      {
         _wall = null;
         _isWall = false;
      }
   }
}