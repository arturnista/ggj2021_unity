using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenshot : MonoBehaviour
{
    
#if UNITY_EDITOR

    private bool _screenshootActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TakeScreenshot();
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            if (!_screenshootActive) StartCoroutine(TakeScreenshotCoroutine());
            else StopAllCoroutines();
            _screenshootActive = !_screenshootActive;
        }
    }

    IEnumerator TakeScreenshotCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        string filename = "SS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        ScreenCapture.CaptureScreenshot("Images/Screenshots/" + filename);
        Debug.Log("SS " + filename + " saved");
    }

#endif

}
