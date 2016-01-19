using UnityEngine;
using System.Collections;
namespace LuminousVector
{
	[RequireComponent(typeof(Animator))]
	public abstract class Entity : MonoBehaviour
	{
		//public
		public float curHeatlh = 100;
		public float maxHealth = 100;
		public float jumpSpeed = 9.8f;
		public int maxJumps = 2;
		public float speed = 10;
		public bool facingRight = true;
		public bool requireGrounded = true;
		public Vector2 groundCheck;
		public float groundCheckRadius = 0.3f;
		public LayerMask groundMask;
		//private
		private Entity _lastAttacker;
		private Rigidbody2D _thisRigidbody;
		private Transform _thisTransform;
		private Vector2 _moveVector;
		private Animator _anim;
		private bool _isGrounded;
		private int _remainingJumps;

		void Start()
		{
			_thisRigidbody = GetComponent<Rigidbody2D>();
			_thisTransform = GetComponent<Transform>();
			_anim = GetComponent<Animator>();
			_remainingJumps = maxJumps;
			
		}

		protected virtual void OnStart()
		{

		}

		//Update
		void Update()
		{
			_moveVector.y = _thisRigidbody.velocity.y; //Get current vertical velocity
			_anim.SetFloat("vSpeed", _moveVector.y);
			_isGrounded = Physics2D.OverlapCircle(new Vector2(_thisTransform.position.x, _thisTransform.position.y) + groundCheck, groundCheckRadius, groundMask);
			_anim.SetBool("isGrounded", _isGrounded);
			if (_isGrounded) //Turn off jump animation when grounded
			{
				_remainingJumps = maxJumps;
			}
			OnUpdate();
			_anim.SetFloat("speed", Mathf.Abs(_moveVector.x));
			_thisRigidbody.velocity = _moveVector; //Apply move vector
		}

		protected abstract void OnUpdate();

		//Jump
		protected void Jump()
		{
			_moveVector.y = jumpSpeed;
			_anim.SetBool("isGrounded", false);
			_remainingJumps--;
		}

		//Move left/right
		protected void Move(int dir)
		{
			if (_isGrounded || !requireGrounded)
			{
				if (dir > 0) //Right
				{
					_moveVector.x = speed;
					if (!facingRight)
						FlipDirection();
				}
				else if (dir < 0) //left
				{
					_moveVector.x = -speed;
					if (facingRight)
						FlipDirection();
				}
				else
				{
					_moveVector.x = 0;
				}
			}
		}

		//Flip the direction of the entity sprite
		protected void FlipDirection()
		{
			facingRight = !facingRight;
			Vector2 scale = _thisTransform.localScale;
			scale.x *= -1;
			_thisTransform.localScale = scale;
		}

		//Draw the gizmo to indicate ground check radius
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + groundCheck, groundCheckRadius);
		}

		//Recieve damage from other entites
		public virtual void RecieveDamage(float damage, Entity src)
		{
			_lastAttacker = src;
			curHeatlh -= Mathf.Abs(damage);
		}

		protected abstract void OnDamaged();
	}
}
