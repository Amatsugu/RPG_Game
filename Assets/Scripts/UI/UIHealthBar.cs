using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace LuminousVector
{
	[ExecuteInEditMode]
	public class UIHealthBar : MonoBehaviour
	{
		//Public
		public Text healthText;
		public Text barName;
		public Image healthBar;
		public Image barFrame;
		public Color normalColor;
		public Color selectedColor;
		public int maxHealth = 200;
		public float barScale = 1;
		public bool selected
		{
			set
			{
				if (value)
					Select();
				else
					DeSelect();
			}
			get { return _selected; }
		}
		public float health
		{
			set
			{
				AddHealth(_health - value);
			}
			get { return _health; }
		}
		public string healthBarName
		{
			set
			{
				barName.text = value;
			}
			get { return barName.text; }
		}

		private bool _selected;
		private float _health;

		void Start()
		{
			transform.localScale = new Vector2(barScale, barScale);
			barFrame.color = normalColor;
		}

		public void AddHealth(float ammount)
		{
			if (ammount < 0)
				RemoveHealth(Mathf.Abs(ammount));
			_health += ammount;
		}

		public void RemoveHealth(float ammount)
		{
			if (ammount < 0)
				RemoveHealth(Mathf.Abs(ammount));
			_health -= ammount;
		}

		public void Select()
		{
			if (_selected)
				return;
			barFrame.color = selectedColor;
			Debug.Log("Select");
			_selected = true;
		}

		public void DeSelect()
		{
			if (!_selected)
				return;
			barFrame.color = normalColor;
			_selected = false;
		}

	}
}
