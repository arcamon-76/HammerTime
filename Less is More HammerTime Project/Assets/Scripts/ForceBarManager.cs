using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceBarManager : MonoBehaviour
{
    public List<Image> images = new List<Image>();

    public void UpdateForceBar(float amount)
    {
        foreach (var k in images)
        {
            k.fillAmount = amount;
        }
    }
}
