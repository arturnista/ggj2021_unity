using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStateMachineAnimation : StateMachineBehaviour
{

	[SerializeField]
	private string _paramName;
	[SerializeField]
	private int _minValue;
	[SerializeField]
	private int _maxValue;

	public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
	{
		animator.SetInteger(_paramName, Random.Range(_minValue, _maxValue));
	}
}
