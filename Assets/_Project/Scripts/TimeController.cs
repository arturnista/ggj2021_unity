using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    
    private static float s_GameTime;
    public static float GameTime => s_GameTime;

    private TextMeshProUGUI _timeText = default;

    private void Awake()
    {
        s_GameTime = 0f;
        _timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        s_GameTime += Time.deltaTime;
        _timeText.text = Format.Time(s_GameTime);
    }

}
