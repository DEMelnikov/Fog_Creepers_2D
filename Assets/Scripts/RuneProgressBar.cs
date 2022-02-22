using UnityEngine;
using UnityEngine.UI;

public class RuneProgressBar : MonoBehaviour
{
    public void UpdateProgress(float newAmount, float maxAmount)
    {
        GetComponent<Image>().fillAmount = newAmount/maxAmount;
    }
}
