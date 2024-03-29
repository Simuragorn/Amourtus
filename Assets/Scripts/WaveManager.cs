using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] protected DayPhaseManager dayPhaseManager;
    [SerializeField] protected IntrudersSpawner spawner;

    protected void Awake()
    {
        dayPhaseManager.OnDayPhaseChanged += DayPhaseManager_OnDayPhaseChanged;
    }

    private void DayPhaseManager_OnDayPhaseChanged(object sender, DayPhaseEnum phase)
    {
        if (phase == DayPhaseEnum.Day)
        {
            spawner.SpawnIntruders();
        }
    }
}
