using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{
    public Caster caster;

    Timer castingTimer;
    Text castText;

    private void Awake()
    {
        castText = GetComponent<Text>();
    }
    private void FixedUpdate()
    {
        GetCastingTimer();
        UpdateCastTime();
    }

    private void GetCastingTimer()
    {
        if(caster.CastList.Count > 0)
        {
            castText.enabled = true;
            castingTimer = caster.CastList[0];
        }
        else
        {
            castText.enabled = false;
            castingTimer = null;
        }
    }
    private void UpdateCastTime()
    {
        if(castingTimer != null)
        {
            castText.text = "Cast: " + castingTimer.TimeLeft.ToString().Substring(0, 3) + "s";
        }
    }
}
