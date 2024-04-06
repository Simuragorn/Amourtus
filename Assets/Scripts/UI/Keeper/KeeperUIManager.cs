using Assets.Scripts.Dto;
using System.Linq;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI smallSoulsCountText;
    [SerializeField] private TextMeshProUGUI mediumSoulsCountText;
    [SerializeField] private TextMeshProUGUI bigSoulsCountText;
    [SerializeField] private TextMeshProUGUI largeSoulsCountText;

    private void Start()
    {
        floorsUIManager.Init(crypt.Floors.ToList());
        guardiansUIManager.Init();

        floorsPageButton.onClick.AddListener(OpenFloorsPage);
        guardiansPageButton.onClick.AddListener(OpenGuardiansPage);
        startNewDayButton.onClick.AddListener(StartNewDay);

        crypt.OnSoulsCountChanged += Crypt_OnSoulsCountChanged;
        SetSoulsCount();
    }

    private void Crypt_OnSoulsCountChanged(object sender, System.EventArgs e)
    {
        SetSoulsCount();
    }

    private void SetSoulsCount()
    {
        smallSoulsCountText.text = crypt.Souls.First(s => s.SoulType == SoulTypeEnum.Small).Amount.ToString();
        mediumSoulsCountText.text = crypt.Souls.First(s => s.SoulType == SoulTypeEnum.Medium).Amount.ToString();
        bigSoulsCountText.text = crypt.Souls.First(s => s.SoulType == SoulTypeEnum.Big).Amount.ToString();
        largeSoulsCountText.text = crypt.Souls.First(s => s.SoulType == SoulTypeEnum.Large).Amount.ToString();
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
