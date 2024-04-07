using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleInsiderItem : MonoBehaviour
{
    [SerializeField] protected Image insiderIcon;
    [SerializeField] protected TextMeshProUGUI insidersCountText;
    protected Insider insider;

    public Insider Insider => insider;

    public void SetInsider(Insider currentInsider, int insidersCount)
    {
        insider = currentInsider;
        insiderIcon.sprite = insider.Configuration.Icon;
        insidersCountText.text = insidersCount.ToString();
    }
}
