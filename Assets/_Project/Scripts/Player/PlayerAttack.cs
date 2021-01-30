using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private BaseWeapon _weapon;
    public BaseWeapon Weapon => _weapon;
    private BaseWeapon _previousWeapon;

    [SerializeField] private BaseWeapon[] _weaponList = default;
    public BaseWeapon[] WeaponList => _weaponList;

    [SerializeField] private AudioClip _changeAkSfx = default;
    private AudioSource _auidoSource;

    private void Awake()
    {
        _auidoSource = GetComponent<AudioSource>();
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
            ChangeWeapon(_weaponList[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && _weapon != _weaponList[1])
        {
            ChangeWeapon(_weaponList[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && _weapon != _weaponList[2])
        {
            ChangeWeapon(_weaponList[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && _weapon != _weaponList[3])
        {
            _auidoSource.PlayOneShot(_changeAkSfx);
            ChangeWeapon(_weaponList[3]);
        }

        if (Input.GetKeyDown(KeyCode.Q) && _weapon != _previousWeapon && _previousWeapon != null)
        {
            if (_previousWeapon == _weaponList[3])
            {
                _auidoSource.PlayOneShot(_changeAkSfx);
            }
            ChangeWeapon(_previousWeapon);
        }
    }

    private void ChangeWeapon(BaseWeapon nextWeapon)
    {
        if (_previousWeapon) _previousWeapon.Disable();
        _previousWeapon = _weapon;
        _weapon.gameObject.SetActive(false);
        _weapon = nextWeapon;
        _weapon.gameObject.SetActive(true);
    }

}
