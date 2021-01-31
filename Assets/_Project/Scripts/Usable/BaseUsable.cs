using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUsable : MonoBehaviour
{
    
    [SerializeField] protected string _action = "";
    public string Action => _action;
    
    public abstract void Use();

}
