using UnityEngine;
using UnityEngine.UI;

public class RuneProgressBar : MonoBehaviour
{


    public void UpdateProgress(float newAmount, float maxAmount)
    {
       // var position = GetComponentInParent<>()
        GetComponent<Image>().fillAmount = newAmount/maxAmount;

    }
}
