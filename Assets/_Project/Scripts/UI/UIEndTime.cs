using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEndTime : MonoBehaviour
{

    private TextMeshProUGUI _timeText = default;

    private void Awake()
    {
        _timeText = GetComponent<TextMeshProUGUI>();
        _timeText.text = "You took " + Format.Time(TimeController.GameTime);
    }
    
}
