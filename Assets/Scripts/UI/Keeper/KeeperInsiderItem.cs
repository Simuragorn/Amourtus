using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeeperInsiderItem : MonoBehaviour
{
    [SerializeField] protected Image insiderIcon;
    [SerializeField] protected TextMeshProUGUI insidersCountText;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    protected InsiderConfiguration insiderConfiguration;

    public event EventHandler<InsiderConfiguration> OnTryAddInsider;
    public event EventHandler<InsiderConfiguration> OnTryRemoveInsider;
    public InsiderConfiguration InsiderConfiguration => insiderConfiguration;

    private void Awake()
    {
        plusButton.onClick.AddListener(TryAddInsider);
        minusButton.onClick.AddListener(TryRemoveInsider);
    }

    public void SetInsider(InsiderConfiguration currentInsider, int insidersCount)
    {
        insiderConfiguration = currentInsider;
        insiderIcon.sprite = insiderConfiguration.Icon;
        insidersCountText.text = insidersCount.ToString();
    }

    private void TryAddInsider()
    {
        OnTryAddInsider?.Invoke(this, insiderConfiguration);
    }
    private void TryRemoveInsider()
    {
        OnTryRemoveInsider?.Invoke(this, insiderConfiguration);
    }
}
