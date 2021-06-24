using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton <UIManager>
{
    [Header("General UI")]
    public TextMeshProUGUI currentPlayerNumber;

    [Header("Dice Rolling UI")]
    public TextMeshProUGUI rollsRemaining;
    public TextMeshProUGUI rollsRemainingText;
    public TextMeshProUGUI clickToRoll;
    


}
