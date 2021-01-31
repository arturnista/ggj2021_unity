using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUse : MonoBehaviour
{

    private Camera _camera;

    private TextMeshProUGUI _usableText;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _usableText = GameObject.FindGameObjectWithTag("UsableText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        CheckUsable();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
    }

    private void CheckUsable()
    {
        _usableText.text = "";
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position + (_camera.transform.forward * .2f), _camera.transform.forward, out hit, 3f))
        {
            BaseUsable usable = hit.transform.GetComponent<BaseUsable>();
            if (usable)
            {
                _usableText.text = usable.Action;
            }
        }
    }

    private void Use()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position + (_camera.transform.forward * .2f), _camera.transform.forward, out hit, 3f))
        {
            BaseUsable usable = hit.transform.GetComponent<BaseUsable>();
            if (usable)
            {
                usable.Use();
            }
        }
    }

}
