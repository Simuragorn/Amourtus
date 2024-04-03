using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private Crypt crypt;
    [SerializeField] private BattleFloorsUIManager floorsUIManager;
    [SerializeField] private BattleGuardiansUIManager guardiansUIManager;
    [SerializeField] private Button floorsPageButton;
    [SerializeField] private Button guardiansPageButton;

    private void Start()
    {
        floorsUIManager.Init(crypt.Floors.ToList());
        guardiansUIManager.Init();

        floorsPageButton.onClick.AddListener(OpenFloorsPage);
        guardiansPageButton.onClick.AddListener(OpenGuardiansPage);
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
