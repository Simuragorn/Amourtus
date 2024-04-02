using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CryptUIManager : MonoBehaviour
{
    [SerializeField] private Crypt crypt;
    [SerializeField] private DayPhaseManager dayPhaseManager;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private FloorsUIManager floorsUIManager;
    [SerializeField] private GuardiansUIManager guardiansUIManager;
    [SerializeField] private Button floorsPageButton;
    [SerializeField] private Button guardiansPageButton;
    [SerializeField] private Button startNewDayButton;

    private void Start()
    {
        floorsUIManager.Init(crypt.Floors.ToList());
        guardiansUIManager.Init();

        floorsPageButton.onClick.AddListener(OpenFloorsPage);
        guardiansPageButton.onClick.AddListener(OpenGuardiansPage);
        startNewDayButton.onClick.AddListener(StartNewDay);
    }

    private void StartNewDay()
    {
        dayPhaseManager.StartNewDay();
    }

    private void OpenFloorsPage()
    {
        guardiansUIManager.gameObject.SetActive(false);
        floorsUIManager.gameObject.SetActive(true);
    }

    private void OpenGuardiansPage()
    {
        floorsUIManager.gameObject.SetActive(false);
        guardiansUIManager.gameObject.SetActive(true);
    }

    void Update()
    {
        bool isUIEnabled = Input.GetKey(KeyCode.Tab);
        if (uiCanvas.gameObject.activeSelf != isUIEnabled)
        {
            uiCanvas.gameObject.SetActive(isUIEnabled);
        }
    }
}
