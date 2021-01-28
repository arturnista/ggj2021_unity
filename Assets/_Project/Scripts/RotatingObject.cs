using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {

	[SerializeField]
	private float _rotateSpeed;
	[SerializeField]
	private Vector3 _axis;

	private void Update ()
	{
		transform.RotateAround(transform.position, _axis, _rotateSpeed * Time.deltaTime);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawRay(transform.position, _axis);
	}

}
