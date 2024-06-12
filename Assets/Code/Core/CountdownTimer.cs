using System;
using UniRx;

public class CountdownTimer : IDisposable
{
    private Action onComplete;

    private bool _isPaused;
    /// <summary>
    /// How long the timer takes to complete from start to finish.
    /// </summary>
    private long _duration;
    /// <summary>
    /// Get how many seconds remain before the timer completes.
    /// </summary>
    public LongReactiveProperty TimeRemaining { get; private set; }
    /// <summary>
    /// Get how many seconds have elapsed since the start of this timer's current cycle.
    /// </summary>
    public LongReactiveProperty TimeElapsed { get; private set; }

    public IDisposable timer;

    public CountdownTimer(int duration, Action onComplete = null)
    {
        this.onComplete = onComplete;
        TimeRemaining = new LongReactiveProperty(duration);
        TimeElapsed = new LongReactiveProperty(0);

        StartNewTimer(duration);
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Stop()
    {
        _isPaused = true;
        TimeRemaining.Value = _duration;
        TimeElapsed.Value = 0;
    }

    public void Resume()
    {
        _isPaused = false;
        if (timer == null)
        {
            timer = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(OnTick);
        }
    }

    public void StartNewTimer(int duration)
    {
        _duration = duration;
        TimeRemaining.Value = _duration;
        TimeElapsed.Value = 0;

        Resume();
    }

    private void OnTick(long time)
    {
        if (_isPaused)
        {
            return;
        }

        TimeElapsed.Value++;
        TimeRemaining.Value--;

        if (TimeRemaining.Value <= 0)
        {
            Stop();
            onComplete?.Invoke();
        }
    }

    public void Dispose()
    {
        onComplete = null;
        timer.Dispose();
    }
}