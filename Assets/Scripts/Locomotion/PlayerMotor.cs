using UnityEngine;
using System.Collections;
namespace LuminousVector
{
	public class PlayerMotor : MonoBehaviour
	{
		//Public
		public float jumpSpeed = 9.8f;
		public float speed = 10;
		public bool facingRight = true;
		public bool requireGrounded = true;

		//Private
		private Rigidbody2D _thisRigidbody;
		private Transform _thisTransform;
		private Vector2 _moveVector;
		private Animator _anim;
		private bool _isGrounded;

		void Start ()
		{
			_thisRigidbody = GetComponent<Rigidbody2D>();
			_thisTransform = GetComponent<Transform>();
			_anim = GetComponent<Animator>();
		}

		void FixedUpdate ()
		{
			_moveVector.y = _thisRigidbody.velocity.y; //Get current vertical velocity
			if (Mathf.Abs(_moveVector.y) <= 0.01f) //Turn off jump animation when grounded
			{
				_anim.ResetTrigger("jump");
				_isGrounded = true;
			}else
			{
				_isGrounded = false;
			}
			//Only allow movement while on the ground
			if (_isGrounded || !requireGrounded)
			{
				//Move left/right
				if (Input.GetKey(KeyCode.D)) //Right
				{
					_moveVector.x = speed;
					if (!facingRight)
						FlipDirection();
				}
				else if (Input.GetKey(KeyCode.A)) //left
				{
					_moveVector.x = -speed;
					if (facingRight)
						FlipDirection();
				}
				else
				{
					_moveVector.x = 0;
				}
				_anim.SetFloat("speed", Mathf.Abs(_moveVector.x));
			}
			//Jump
			if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
			{
				_moveVector.y = jumpSpeed;
				_anim.SetTrigger("jump");
			}
			
			_thisRigidbody.velocity = _moveVector; //Apply move vector
		}

		//Flip sprite direction
		void FlipDirection()
		{
			facingRight = !facingRight;
			Vector2 scale = _thisTransform.localScale;
			scale.x *= -1;
			_thisTransform.localScale = scale;
		}
	}
}
