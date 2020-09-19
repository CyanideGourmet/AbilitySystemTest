using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float TimeLeft = 0f;
    public UnityEvent onTimeout = new UnityEvent();

    bool isStopped = false;

    private void Awake()
    {
        onTimeout.AddListener(() => TimeLeft = 0f);
        onTimeout.AddListener(() => Destroy(this));
    }

    public void StartTimer() => StartCoroutine(CountdownAsync());
    public void StopTimer() => isStopped = true;

    IEnumerator CountdownAsync()
    {
        while((TimeLeft -= Time.deltaTime) > 0)
        {
            if (isStopped) break;
            yield return null;
        }
        if (!isStopped) onTimeout.Invoke();
        yield return null;
    }
}
