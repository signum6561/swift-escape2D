using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public static CameraFollow Instance;
	public float zOffSet = -5f;

	public Transform target;

	private Vector2 velocity = Vector2.zero;
	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}
	private void FixedUpdate()
	{
		if(target != null)
			transform.position = new Vector3(target.transform.position.x, target.transform.position.y, zOffSet);
	}
}

