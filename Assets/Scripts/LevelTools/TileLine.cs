using UnityEngine;
using System.Collections.Generic;
namespace LuminousVector
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(BoxCollider2D))]
	public class TileLine : MonoBehaviour
	{
		//Public
		public Sprite sprite;
		public Direction direction;
		public int size;
		public bool isTrigger;
		//Private
		private List<GameObject> _tiles;
		private BoxCollider2D _collider;

		void Start()
		{
			_tiles = new List<GameObject>();
			_collider = GetComponent<BoxCollider2D>();
		}

		void Update ()
		{
			if (Application.isPlaying)
				enabled = false;
			if (size < 0)
				size = 0;
			ClearTiles();
			for(int i = 0; i < size; i++)
			{
				GameObject tile = new GameObject();
				SpriteRenderer sp = tile.AddComponent<SpriteRenderer>();
				sp.sprite = sprite;
				GameObject curTile = Instantiate(tile, new Vector2(i, 0), Quaternion.identity) as GameObject;
				curTile.transform.parent = transform;
			}
		}

		void ClearTiles()
		{
			for(int i = _tiles.Count-1; i >= 0; i--)
			{
				Destroy(_tiles[i]);
			}
			_tiles = new List<GameObject>();
		}
	}
}
