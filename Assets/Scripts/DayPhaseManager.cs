using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DayPhaseEnum
{
    None,
    Day,
    Night,
    LateNight
}

public class DayPhaseManager : MonoBehaviour
{
    [SerializeField] protected float phaseDurationInSeconds;
    [SerializeField] protected Transform risePoint;
    [SerializeField] protected Transform endOfTheDayPhasePoint;
    [SerializeField] protected Transform endOfTheNightPhasePoint;

    [SerializeField] protected GameObject sun;
    [SerializeField] protected GameObject moon;

    protected DayPhaseEnum currentPhase;
    protected float risingSpeed;

    public event EventHandler<DayPhaseEnum> OnDayPhaseChanged;
    public DayPhaseEnum CurrentPhase => currentPhase;

    private void Awake()
    {
        risingSpeed = (endOfTheDayPhasePoint.position.y - risePoint.position.y) / phaseDurationInSeconds;
        currentPhase = DayPhaseEnum.None;
    }

    protected void Start()
    {
        ResetDayPhase();
    }

    private void ResetDayPhase()
    {
        sun.transform.position = risePoint.position;
        moon.transform.position = risePoint.position;

        currentPhase = DayPhaseEnum.None;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPhase == DayPhaseEnum.Night || currentPhase == DayPhaseEnum.LateNight)
            {
                ResetDayPhase();
            }
        }
        UpdatePhase();
        Vector2 translation = risingSpeed * Time.deltaTime * Vector2.up;
        switch (currentPhase)
        {
            case DayPhaseEnum.Day:
                sun.transform.Translate(translation);
                break;
            case DayPhaseEnum.Night:
                moon.transform.Translate(translation);
                break;
            default:
                break;
        }
    }

    protected void UpdatePhase()
    {
        DayPhaseEnum newPhase;
        if (sun.transform.position.y < endOfTheDayPhasePoint.position.y)
        {
            newPhase = DayPhaseEnum.Day;
        }
        else if (moon.transform.position.y < endOfTheNightPhasePoint.position.y)
        {
            newPhase = DayPhaseEnum.Night;
        }
        else
        {
            newPhase = DayPhaseEnum.LateNight;
        }
        if (currentPhase != newPhase)
        {
            OnDayPhaseChanged?.Invoke(this, newPhase);
        }
        currentPhase = newPhase;
    }
}
