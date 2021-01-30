using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUsable : MonoBehaviour
{
    
    [SerializeField] private string _action = "";
    public string Action => _action;
    
    public abstract void Use();

}
