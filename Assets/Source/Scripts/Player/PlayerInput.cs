using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
   public Action<Vector2> OnChangeDirection;
   public Action OnJumpKeyPressed;      
   public Action OnAttackKeyPressed;      
   
   [SerializeField] private KeyCode _moveLeft, _moveRight, _jumpKey, _attackKey;
   private Vector2 _moveDirection;   
   
   private void Start()
   {
      _moveLeft = KeyCode.A;
      _moveRight = KeyCode.D;
      _jumpKey = KeyCode.W;
   }
   
   private void Update()   
   {
      if (Input.GetKey(_moveLeft))
      {
         _moveDirection = Vector2.left;
         OnChangeDirection?.Invoke(_moveDirection);
      }
      
      if (Input.GetKey(_moveRight))
      {
         _moveDirection = Vector2.right;
         OnChangeDirection?.Invoke(_moveDirection);
      }
      
      if (Input.GetKeyDown(_jumpKey))
         OnJumpKeyPressed?.Invoke();
      

      if (Input.GetKeyDown(_attackKey))
         OnAttackKeyPressed?.Invoke();
   }
}
