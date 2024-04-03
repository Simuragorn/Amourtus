using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeeperUIManager : MonoBehaviour
{
    [SerializeField] private Button startNewDayButton;
    [SerializeField] private DayPhaseManager dayPhaseManager;

    [SerializeField] private Crypt crypt;
    [SerializeField] private KeeperFloorsUIManager floorsUIManager;
    [SerializeField] private KeeperGuardiansUIManager guardiansUIManager;
    [SerializeField] private Button floorsPageButton;
    [SerializeField] private Button guardiansPageButton;

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
}
