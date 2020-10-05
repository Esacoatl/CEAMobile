using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeZero : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
