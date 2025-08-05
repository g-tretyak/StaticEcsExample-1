using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerModule
{
    public float timerMax;
    public float timer;

    public List<Action> actionsOnEnd;

    public TimerModule(float timerMaxVal)
    {
        timer = 0f;
        timerMax = timerMaxVal;
        actionsOnEnd = new List<Action>();
    }

    public bool OnTimer => timer > 0f;

    public void SetTimer()
    {
        timer = timerMax;
    }

    public void AddAction(Action action)
    {
        actionsOnEnd.Add(action);
    }

    public void TickTimer()
    {
        if (timer <= 0f) return;
        timer -= Time.deltaTime;

        if (timer > 0f) return;
        DoActionsOnEnd();
    }

    public void SkipTimer()
    {
        if (timer <= 0f) return;
        timer = 0f;

        DoActionsOnEnd();
    }

    private void DoActionsOnEnd()
    {
        foreach (var a in actionsOnEnd) a?.Invoke();

        actionsOnEnd.Clear();
    }
}