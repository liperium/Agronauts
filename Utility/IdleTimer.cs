using Godot;

public class IdleTimer
{
    private ulong endTime;
    private ulong remainingTime;
    
    public IdleTimer() {}
    public IdleTimer(float durationInSeconds)
    {
        Start(durationInSeconds);
    }

    public void Start(float durationInSeconds)
    {
        endTime = Time.GetTicksMsec() + (ulong)(durationInSeconds * 1000f);
    }

    public bool IsStarted()
    {
        return endTime > 0;
    }

    /// <summary>
    /// Checks if the timer is started and not elapsed (does not check if it's stopped).
    /// </summary>
    public bool IsStartedNotElapsed()
    {
        return IsStarted() && !IsElapsed();
    }

    public bool IsElapsed()
    {
        return Time.GetTicksMsec() >= endTime;
    }

    public void Stop()
    {
        if (IsStopped())
        {
            return;
        }
        
        if (IsElapsed())
        {
            remainingTime = endTime;
            return;
        }
        
        remainingTime = endTime - Time.GetTicksMsec();
    }

    public void Resume()
    {
        if (!IsStopped())
        {
            return;
        }
        
        endTime = Time.GetTicksMsec() + remainingTime;
        remainingTime = 0;
    }

    public bool IsStopped()
    {
        return remainingTime > 0;
    }

    /// <summary>
    /// Returns true if timer is started and not stopped (aka running).
    /// </summary>
    public bool IsRunning()
    {
        return IsStarted() && !IsStopped();
    }
}