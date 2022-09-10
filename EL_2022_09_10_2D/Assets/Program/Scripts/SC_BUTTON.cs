using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_BUTTON : MonoBehaviour
{
    Image mainImage;
    float blinkAlpha = 1.0f;
    float blinkSpeed = 2.0f;
    float blinkSign = 1.0f;
    bool isSelect = false;

    void Start()
    {
        mainImage = gameObject.GetComponent<Image>();
    }

    public void ButtonUpdate()
    {

        blinkAlpha += blinkSpeed * blinkSign * Time.deltaTime;

        if (blinkAlpha >= 1.0f || blinkAlpha <= 0.0f)
            blinkSign *= -1.0f;

        mainImage.color = new Color(mainImage.color.r, mainImage.color.g, mainImage.color.b, blinkAlpha);
    }

    public void SetIsSelect(bool select)
    {
        if(select && !isSelect)
        {
            mainImage.color = new Color(1.0f, 0.5f, 0.0f, blinkAlpha);
        }
        else if(!select)
        {
            mainImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            blinkAlpha = 1.0f;
            blinkSign = 1.0f;
        }
    }
}
