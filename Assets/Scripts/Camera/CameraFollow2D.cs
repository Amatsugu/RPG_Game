using UnityEngine;
using System.Collections;
namespace LuminousVector
{
	public class CameraFollow2D : MonoBehaviour
	{
		//Public
		public Vector2 offset;
		public bool lerp;
		public float lerpSpeed;
		public Transform target;

		void Start()
		{
			if (target == null)
			{
				Debug.LogError("CameraFollow: no target set");
				enabled = false;
			}
		}

		void Update()
		{
			Vector3 pos = target.position;
			//Apply pan offset
			pos.x += offset.x;
			pos.y += offset.y;
			pos.z = transform.position.z;
			if (lerp) //Smooth panning
				transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * lerpSpeed);
			else //Hard panning
				transform.position = pos;
		}
	}
}
