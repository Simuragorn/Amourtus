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

    [SerializeField] protected SpriteRenderer dayPhaseLight;
    [SerializeField] protected Color dayLightColor;
    [SerializeField] protected Color nightLightColor;
    [SerializeField] protected Color noLightColor;

    [SerializeField] protected float phaseProgress;

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
        SetLateNight();
    }

    public void StartNewDay()
    {
        sun.transform.position = risePoint.position;
        moon.transform.position = risePoint.position;

        currentPhase = DayPhaseEnum.None;
    }

    private void SetLateNight()
    {
        sun.transform.position = endOfTheDayPhasePoint.position;
        moon.transform.position = endOfTheNightPhasePoint.position;

        currentPhase = DayPhaseEnum.LateNight;
    }

    protected void Update()
    {
        UpdatePhase();
        UpdatePhaseLight();
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
            phaseProgress = sun.transform.position.y / endOfTheDayPhasePoint.position.y;
        }
        else if (moon.transform.position.y < endOfTheNightPhasePoint.position.y)
        {
            newPhase = DayPhaseEnum.Night;
            phaseProgress = moon.transform.position.y / endOfTheNightPhasePoint.position.y;
        }
        else
        {
            newPhase = DayPhaseEnum.LateNight;
            phaseProgress = 1;
        }
        if (currentPhase != newPhase)
        {
            OnDayPhaseChanged?.Invoke(this, newPhase);
        }
        currentPhase = newPhase;
    }

    protected void UpdatePhaseLight()
    {
        Color from;
        Color to;
        if (CurrentPhase == DayPhaseEnum.Day)
        {
            to = noLightColor;
            from = dayLightColor;
        }
        else
        {
            from = noLightColor;
            to = nightLightColor;
        }
        dayPhaseLight.color = Color.Lerp(from, to, phaseProgress);
    }
}
