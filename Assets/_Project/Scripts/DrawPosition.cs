using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPosition : MonoBehaviour {

	[SerializeField]
	private Vector3 m_Size = Vector3.one;
	[SerializeField]
	private Vector3 _offset = Vector3.zero;
	[SerializeField]
	private Color _color = Color.red;

	void OnDrawGizmos()
	{
		Gizmos.color = _color;
		Gizmos.DrawWireCube(transform.position + _offset, m_Size);
	}

}
