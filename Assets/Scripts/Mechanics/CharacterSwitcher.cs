using UnityEngine;
using System.Collections.Generic;
namespace LuminousVector
{
	public class CharacterSwitcher : MonoBehaviour
	{
		//Public
		public List<Character> characters;
		public int selected
		{
			set
			{
				_selected = (value > characters.Count-1) ? 0 : value;
				foreach (Character c in characters)
					c.healthBar.DeSelect();
				characters[_selected].healthBar.Select();
			}
			get { return _selected; }
		}

		//Private
		private int _selected;

		void Update ()
		{
			if(Input.GetKeyDown(KeyCode.Tab))
			{
				selected++;
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
				selected = 1;
			else if (Input.GetKeyDown(KeyCode.Alpha2))
				selected = 2;
			else if (Input.GetKeyDown(KeyCode.Alpha3))
				selected = 3;
			else if (Input.GetKeyDown(KeyCode.Alpha4))
				selected = 4;
		}
	}
}
