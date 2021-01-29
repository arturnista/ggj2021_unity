using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIWeaponItem : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _weaponText = default;
    [SerializeField] private GameObject _select = default;

    private BaseWeapon _weapon;
    public BaseWeapon Weapon => _weapon;

    private void Awake()
    {
        Deselect();
    }

    public void Construct(int index, BaseWeapon weapon)
    {
        _weapon = weapon;
        _weaponText.text = (index + 1).ToString();
    }

    public void Select()
    {
        _select.SetActive(true);
    }

    public void Deselect()
    {
        _select.SetActive(false);
    }

}
