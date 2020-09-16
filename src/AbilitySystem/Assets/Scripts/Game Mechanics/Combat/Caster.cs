using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    public List<Timer> CastList
    {
        get
        {
            return castList;
        }
    }

    List<Timer> castList = new List<Timer>();
    int maxQueuedCasts = 2;
    public void Cast(Active ability)
    {
        if(castList.Count < maxQueuedCasts)
        {
            float timeToThisCast = 0f;
            if (castList.Count != 0) timeToThisCast = castList[0].TimeLeft;
            Timer newTimer = gameObject.AddComponent<Timer>();
            castList.Add(newTimer);
            newTimer.TimeLeft = ability.CastTime + timeToThisCast;
            newTimer.onTimeout.AddListener(ability.Use);
            newTimer.onTimeout.AddListener(() => Destroy(newTimer));
            newTimer.onTimeout.AddListener(() => castList.Remove(newTimer));
            newTimer.StartTimer();
        }
    }

    public void WipeCastQueue()
    {
        foreach(Timer timer in castList)
        {
            timer.StopTimer();
            Destroy(timer);
        }
        castList.Clear();
    }
}
