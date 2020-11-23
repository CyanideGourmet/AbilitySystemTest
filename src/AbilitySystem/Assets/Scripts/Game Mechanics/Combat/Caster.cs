using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    public List<Timer> CastList
    {
        get
        {
            return _castList;
        }
    }

    int maxQueuedCasts = 2;
    public void Cast(Active ability)
    {
        if(_castList.Count < maxQueuedCasts)
        {
            float timeToThisCast = 0f;
            if (_castList.Count != 0) timeToThisCast = _castList[0].TimeLeft;

            Timer newTimer = gameObject.AddComponent<Timer>();
            _castList.Add(newTimer);
            newTimer.onTimeout.AddListener(ability.Use);
            newTimer.onTimeout.AddListener(() => _castList.Remove(newTimer));

            newTimer.TimeLeft = ability.CastTime + timeToThisCast;
            newTimer.StartTimer();
        }
    }
    public void WipeCastQueue()
    {
        foreach(Timer timer in _castList)
        {
            timer.StopTimer();
            Destroy(timer);
        }
        _castList.Clear();
    }

    List<Timer> _castList = new List<Timer>();
}
