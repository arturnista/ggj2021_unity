using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private BaseWeapon _weapon;
    private BaseWeapon _previousWeapon;
    [SerializeField] private BaseWeapon[] _weaponList = default;

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
        
        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _weapon != _weaponList[0])
        {
            _previousWeapon = _weapon;
            _weapon.gameObject.SetActive(false);
            _weapon = _weaponList[0];
            _weapon.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && _weapon != _weaponList[1])
        {
            _previousWeapon = _weapon;
            _weapon.gameObject.SetActive(false);
            _weapon = _weaponList[1];
            _weapon.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && _weapon != _weaponList[2])
        {
            _previousWeapon = _weapon;
            _weapon.gameObject.SetActive(false);
            _weapon = _weaponList[2];
            _weapon.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && _weapon != _weaponList[3])
        {
            _previousWeapon = _weapon;
            _weapon.gameObject.SetActive(false);
            _weapon = _weaponList[3];
            _weapon.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && _weapon != _previousWeapon && _previousWeapon != null)
        {
            var tempPreviousWeapon = _weapon;
            _weapon.gameObject.SetActive(false);
            _weapon = _previousWeapon;
            _previousWeapon = tempPreviousWeapon;
            _weapon.gameObject.SetActive(true);
        }
    }

}
