using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BattleUIManager battleUIManager;
    [SerializeField] private KeeperUIManager keeperUIManager;
    [SerializeField] private GameManager gameManager;

    private GameObject suitableUIManager;

    private void Awake()
    {
        gameManager.OnStateChanged += GameManager_OnStateChanged;
        suitableUIManager = keeperUIManager.gameObject;
    }

    private void GameManager_OnStateChanged(object sender, GameStateEnum e)
    {
        battleUIManager.gameObject.SetActive(false);
        keeperUIManager.gameObject.SetActive(false);
        if (e == GameStateEnum.Battle)
        {
            suitableUIManager = battleUIManager.gameObject;
        }
        else if (e == GameStateEnum.Keep)
        {
            suitableUIManager = keeperUIManager.gameObject;
        }

    }

    void Update()
    {
        bool isUIEnabled = Input.GetKey(KeyCode.Tab);

        if (suitableUIManager.activeSelf != isUIEnabled)
        {
            suitableUIManager.SetActive(isUIEnabled);
        }
    }
}
