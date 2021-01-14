using UnityEngine;

public class BeatChecker : VibingEntity
{
    public static BeatChecker instance;
    public float toleranceRange = 0.07f;

    /// <summary>
    /// Checks if current time is in musik beat.
    /// </summary>
    /// <returns>true if Time.time is in beat</returns>
    public bool IsInBeat()//is current time in beat?
    {
        return IsInBeat(Time.time, toleranceRange);
    }

    /// <summary>
    /// Checks if given time is in musik beat.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="toleranceRange"></param>
    /// <returns>true if time is in beat</returns>
    public bool IsInBeat(float time, float toleranceRange)
    {
        // time is in beat if: time == beatStart + n * beatLegnth + -toleranceRange; where n is any natural number
        float missedBySeconds = (time - beatStart) % beatLength;
        return missedBySeconds <= toleranceRange || missedBySeconds >= beatLength - toleranceRange;
    }

    /// <summary>
    /// Checks if given time is in musik beat.
    /// </summary>
    /// <param name="time"></param>
    /// <returns>true if time is in beat</returns>
    public bool IsInBeat(float time)
    {
        return IsInBeat(time, toleranceRange);
    }

    public override void OnBeat(float bps)
    {
        beatStart = Time.time;
        beatLength = 1 / bps;
    }

    private void Awake()
    {
        instance = this;
    }

    //Hold Beat
    float beatStart = 0;
    float beatLength = 0;
}
