using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BattleUIManager battleUIManager;
    [SerializeField] private KeeperUIManager keeperUIManager;

    [SerializeField] private DayPhaseManager dayPhaseManager;
    private bool isBattle = false;

    private void Awake()
    {
        dayPhaseManager.OnDayPhaseChanged += DayPhaseManager_OnDayPhaseChanged;
    }

    private void DayPhaseManager_OnDayPhaseChanged(object sender, DayPhaseEnum e)
    {
        ChangeGameState(e != DayPhaseEnum.LateNight);
    }

    void Update()
    {
        bool isUIEnabled = Input.GetKey(KeyCode.Tab);
        GameObject neededUI = isBattle ? battleUIManager.gameObject : keeperUIManager.gameObject;
        if (neededUI.gameObject.activeSelf != isUIEnabled)
        {
            neededUI.gameObject.SetActive(isUIEnabled);
        }
    }

    private void ChangeGameState(bool isBattleStarted)
    {
        isBattle = isBattleStarted;
        battleUIManager.gameObject.SetActive(false);
        keeperUIManager.gameObject.SetActive(false);
    }
}
