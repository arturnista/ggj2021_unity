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
        UpdateList();
    }

    private void OnEnable()
    {
        _playerAttack.OnWeaponUpdate += UpdateList;
    }

    private void OnDisable()
    {
        _playerAttack.OnWeaponUpdate -= UpdateList;
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

    private void UpdateList()
    {
        _items = new List<UIWeaponItem>();
        _playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--) {
            Destroy( transform.GetChild(i).gameObject );
        }

        for (int i = 0; i < _playerAttack.WeaponList.Length; i++)
        {
            if (!_playerAttack.WeaponList[i].IsAvailable) continue;
            UIWeaponItem weapon = Instantiate(_itemPrefab, transform).GetComponent<UIWeaponItem>();
            weapon.Construct(i, _playerAttack.WeaponList[i]);

            _items.Add(weapon);
        }
    }

}
