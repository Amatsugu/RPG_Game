using UnityEngine;
using System.Collections;
using System;

namespace LuminousVector
{
	public abstract class Character : Entity
	{
		//Public
		public UIHealthBar healthBar;

		protected override void OnDamaged()
		{
			healthBar.health = curHeatlh;
		}
	}
}
