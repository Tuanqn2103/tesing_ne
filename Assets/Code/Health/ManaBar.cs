using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    public Image fillBar;
    public void updateBar(int currentValue, int maxValue){
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
    }
}
