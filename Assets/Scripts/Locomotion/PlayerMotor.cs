using UnityEngine;
using UnityEditor;
using System.Collections;
namespace LuminousVector
{
	[ExecuteInEditMode]
	public class PlayerMotor : MonoBehaviour
	{
		//Public
		public float jumpSpeed = 9.8f;
		public int maxJumps = 2;
		public float speed = 10;
		public bool facingRight = true;
		public bool requireGrounded = true;
		public Vector2 groundCheck;
		public float groundCheckRadius;
		public LayerMask groundMask;
		public float decleration;

		//Private
		private Rigidbody2D _thisRigidbody;
		private Transform _thisTransform;
		private Vector2 _moveVector;
		private Animator _anim;
		private bool _isGrounded;
		private int _remainingJumps;

		void Start ()
		{
			_thisRigidbody = GetComponent<Rigidbody2D>();
			_thisTransform = GetComponent<Transform>();
			_anim = GetComponent<Animator>();
			_remainingJumps = maxJumps;
		}

		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(new Vector2(_thisTransform.position.x, _thisTransform.position.y) + groundCheck, groundCheckRadius);
		}

		void Update ()
		{
			_moveVector.y = _thisRigidbody.velocity.y; //Get current vertical velocity
			_anim.SetFloat("vSpeed", _moveVector.y);
			_isGrounded = Physics2D.OverlapCircle(new Vector2(_thisTransform.position.x, _thisTransform.position.y) + groundCheck, groundCheckRadius, groundMask);
			_anim.SetBool("isGrounded", _isGrounded);
			if (_isGrounded) //Turn off jump animation when grounded
			{
				_remainingJumps = maxJumps;
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
					//(_moveVector.x > 0.01f) ? _moveVector.x - Time.fixedDeltaTime * decleration : (_moveVector.x < -0.0f) ? _moveVector.x + Time.fixedDeltaTime * decleration : 0;
				}
				_anim.SetFloat("speed", Mathf.Abs(_moveVector.x));
			}
			//Jump
			if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || _remainingJumps > 0))
			{
				Debug.Log("jump");
				_moveVector.y = jumpSpeed;
				_anim.SetBool("isGrounded", false);
				_remainingJumps--;
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
