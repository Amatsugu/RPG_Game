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
		public int maxHealth = 200;
		public float barScale = 1;
		public string healthBarName
		{
			set
			{
				barName.text = value;
			}
			get
			{
				return barName.text;
			}
		}

		void Start()
		{
			transform.localScale = new Vector2(barScale, barScale);
		}

		void Update()
		{
			Start();
		}

		public void AddHealth(float ammount)
		{
			if (ammount < 0)
				RemoveHealth(Mathf.Abs(ammount));
		}

		public void RemoveHealth(float ammount)
		{
			if (ammount < 0)
				RemoveHealth(Mathf.Abs(ammount));
		}


	}
}
