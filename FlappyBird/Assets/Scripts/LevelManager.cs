using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField]
	private float speed;

	internal Coroutine levelRoll;

	private void Start()
	{
		levelRoll = StartCoroutine(RollCoroutine());
	}

	internal IEnumerator RollCoroutine()
	{
		while (true)
		{
			yield return null;
			transform.Translate(new Vector2(-speed, 0) * Time.deltaTime);
		}
	}
}