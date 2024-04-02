using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntruderItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI intruderNameText;
    [SerializeField] protected TextMeshProUGUI intrudersCountText;
    protected Intruder intruder;

    public Intruder Intruder => intruder;

    public void SetIntruder(Intruder currentIntruder, int intrudersCount)
    {
        intruder = currentIntruder;
        intruderNameText.text = intruder.Configuration.CharacterName;
        intrudersCountText.text = intrudersCount.ToString();
    }
}
