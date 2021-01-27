using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private BaseWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInChildren<BaseWeapon>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _weapon.StartAttack();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _weapon.StopAttack();
        }
    }

}
