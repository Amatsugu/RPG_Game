using UnityEngine;
using System.Collections;
namespace LuminousVector
{
	public class Player : Character
	{
		protected override void OnUpdate ()
		{
			int dir = Input.GetKey(KeyCode.A) ? -1 : (Input.GetKey(KeyCode.D)) ? 1 : 0;
			Move(dir);
			if (Input.GetKeyDown(KeyCode.Space))
				Jump();
		}
	}
}
