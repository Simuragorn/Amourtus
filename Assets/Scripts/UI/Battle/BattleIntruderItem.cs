using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleIntruderItem : MonoBehaviour
{
    [SerializeField] protected Image intruderIcon;
    [SerializeField] protected TextMeshProUGUI intrudersCountText;
    protected Intruder intruder;

    public Intruder Intruder => intruder;

    public void SetIntruder(Intruder currentIntruder, int intrudersCount)
    {
        intruder = currentIntruder;
        intruderIcon.sprite = intruder.Configuration.Icon;
        intrudersCountText.text = intrudersCount.ToString();
    }
}
