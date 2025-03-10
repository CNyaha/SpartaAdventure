using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;


    private Color imgColor;
    private Coroutine coroutine;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += HitFlash;
        CharacterManager.Instance.Player.condition.onHealing += HealFlash;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitFlash()
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        image.enabled = true;
        imgColor = new Color(1f, 100f / 255f, 100f / 255f);
        image.color = imgColor;
        coroutine = StartCoroutine(FadeAway());
    }

    public void HealFlash()
    {
        if(coroutine != null) 
            StopCoroutine(coroutine);

        image.enabled = true;
        imgColor = new Color(100f / 255f, 1f, 100f / 255f);
        image.color = imgColor;
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 90 / 255f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(imgColor.r, imgColor.g, imgColor.b, a);
            yield return null;
        }

        image.enabled = false;
    }

}
