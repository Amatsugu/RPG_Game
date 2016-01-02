using UnityEngine;
using System.Collections;
namespace LuminousVector
{
	public class PlayerMotor : MonoBehaviour
	{
		//Public
		public float gravity = 9.8f;
		public float speed = 10;

		//Private
		private Rigidbody2D _thisRigidbody;
		private Transform _thisTransform;
		private Vector2 _moveVector;

		void Start ()
		{
			_thisRigidbody = GetComponent<Rigidbody2D>();
			_thisTransform = GetComponent<Transform>();
		}

		void FixedUpdate ()
		{
			_moveVector.y -= gravity * Time.fixedDeltaTime;
			_thisRigidbody.velocity = _moveVector;
			_thisRigidbody.simulated = true;
		}
	}
}
