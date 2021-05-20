using UnityEngine;

public class BeatChecker : VibingEntity
{
    public static BeatChecker instance;

    public float toleranceShift = 0.00f;
    public float toleranceRange = 0.07f;

    //Hold Beat
    float beatStart = 0;
    private float beatLength = 0;
    public float BeatLength
    {
        get => beatLength;
    }

    //Callibration + UI
    private bool toggle = false;
    private int hitcount = 0;
    private int misscount = 0;
    private float missedBY = 0;
    private float hitAVG = 0;
    private bool isSet = false;

    void Update()
    {
        //Toggle UI
        if (Input.GetKeyDown(KeyCode.N))
            {
            toggle = !toggle;
            if (toggle) {
                //RESET SHIFT
                toleranceShift = 0.00f;
                hitcount = 0;
                misscount = 0;
                hitAVG = 0;
                isSet = false;
            }
        }

        //GiVE INPUT TO
        if (Input.GetKeyDown(KeyCode.M)){
            if (toggle && !isSet)
            {
                //missedBY = (Time.time - beatStart);
                missedBY = (Time.time - beatStart) % beatLength;

                if (missedBY <= (toleranceRange + toleranceShift))
                {
                    hitAVG += missedBY;
                    hitcount = hitcount + 1;
                }
                else if (missedBY >= (beatLength - (toleranceRange - toleranceShift)))
                {
                    hitAVG -= (beatLength - missedBY);
                    hitcount = hitcount + 1;
                }
                else
                {
                    misscount = misscount + 1;
                }
                if (hitcount == 10) {
                    //sollte nur einmal setzen
                    toleranceShift += (hitAVG / 10);
                    isSet = true;
                }
            }
        }
    }

        /// <summary>
        /// Checks if current time is in musik beat.
        /// </summary>
        /// <returns>true if Time.time is in beat</returns>
        public bool IsInBeat()//is current time in beat?
    {
        return IsInBeat(Time.time, toleranceRange, toleranceShift);
    }

    /// <summary>
    /// Checks if given time is in musik beat.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="toleranceRange"></param>
    /// <returns>true if time is in beat</returns>
    public bool IsInBeat(float time, float toleranceRange, float toleranceShift)
    {
        // time is in beat if: time == beatStart + n * beatLegnth + -toleranceRange; where n is any natural number
        float missedBySeconds = (time - beatStart) % beatLength;
        return missedBySeconds <= (toleranceRange + toleranceShift) || missedBySeconds >= beatLength - (toleranceRange - toleranceShift);
        //return missedBySeconds <= toleranceRange || missedBySeconds >= beatLength - toleranceRange; old
    }


    public void OnGUI()
    {
        if (toggle)
        {
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(10, 25, 200, 500), "HIT_COUNT: " + hitcount);
            GUI.Label(new Rect(10, 50, 200, 500), "MISS_COUNT: " + misscount);
            GUI.Label(new Rect(10, 75, 200, 500), "CURRENT_SHIFT: " + toleranceShift);
            GUI.Label(new Rect(10, 100, 200, 500), "CURRENT_RANGE: " + toleranceRange);
            GUI.Label(new Rect(10, 125, 200, 500), "BEAT: " + beatStart);
            GUI.Label(new Rect(10, 150, 200, 500), "TIME: " + Time.time);
            GUI.Label(new Rect(10, 175, 200, 500), "DIFF: " + (Time.time - beatStart));
            GUI.Label(new Rect(10, 200, 200, 500), "MISSED_BY: " + missedBY);
            GUI.Label(new Rect(10, 225, 200, 500), "HIT_AVG: " + hitAVG);
            GUI.Label(new Rect(10, 250, 200, 500), "SHIFT_POS: " + (toleranceRange + toleranceShift));
            GUI.Label(new Rect(10, 275, 200, 500), "SHIFT_NEG: " + (toleranceRange - toleranceShift));
            GUI.Label(new Rect(10, 300, 200, 500), "ADJUSTED: " + isSet);
        }
    }

    /// <summary>
    /// Checks if given time is in musik beat.
    /// </summary>
    /// <param name="time"></param>
    /// <returns>true if time is in beat</returns>
    public bool IsInBeat(float time)
    {
        return IsInBeat(time, toleranceRange, toleranceRange);
    }

    public float IsInBeatMissedBy(float time)
    {
        return (time - beatStart) % beatLength;
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        beatLength = 1 / bps;
        Debug.Log("Jitter_delay is " +jitter_delay+" beatlength is "+ beatLength, this);
    }

    public void SetBeatStart(float beatStart)
    {
        Debug.Log("beatStart is "+beatStart);
        this.beatStart = beatStart;
    }

    private void Awake()
    {
        instance = this;
    }

}
