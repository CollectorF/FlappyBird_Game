using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField]
	private float speed;
	[SerializeField]
	private GameObject floor;

	internal Coroutine levelRoll;
	private Vector3 floorStartPosition;

	private void Start()
	{
		floorStartPosition = floor.transform.position;
		levelRoll = StartCoroutine(RollCoroutine());
	}

    internal IEnumerator RollCoroutine()
	{
		var width = Camera.main.orthographicSize * 2;
		while (true)
		{
			yield return null;
			transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
			if (floor.transform.position.x <= -width)
			{
				floor.transform.position = floorStartPosition;
			}
		}
	}
}