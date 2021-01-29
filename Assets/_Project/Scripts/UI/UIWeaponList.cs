using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponList : MonoBehaviour
{
    
    [SerializeField] private GameObject _itemPrefab = default;

    private PlayerAttack _playerAttack;
    private List<UIWeaponItem> _items;

    private void Awake()
    {
        _items = new List<UIWeaponItem>();
        
        _playerAttack = GameObject.FindObjectOfType<PlayerAttack>();

        for (int i = 0; i < _playerAttack.WeaponList.Length; i++)
        {
            UIWeaponItem weapon = Instantiate(_itemPrefab, transform).GetComponent<UIWeaponItem>();
            weapon.Construct(i, _playerAttack.WeaponList[i]);

            _items.Add(weapon);
        }
    }

    private void Update()
    {
        
        foreach (var item in _items)
        {
            if (item.Weapon == _playerAttack.Weapon)
            {
                item.Select();
            }
            else
            {
                item.Deselect();
            }
        }
    }

}
