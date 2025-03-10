using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    private void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>();

        foreach (Image img in images)
        {
            if (img.type == Image.Type.Filled)
            {
                uiBar = img;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtrect(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }

}
