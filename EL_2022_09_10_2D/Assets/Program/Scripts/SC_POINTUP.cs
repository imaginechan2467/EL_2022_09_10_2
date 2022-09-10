using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SC_POINTUP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    float upSpeed = 100.0f;
    float alphaSpeed = 0.5f;

    void Start()
    {
    }

    void Update()
    {
        transform.position += new Vector3(0.0f, upSpeed * Time.deltaTime, 0.0f);
        gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Max(gameObject.GetComponent<CanvasGroup>().alpha - alphaSpeed * Time.deltaTime, 0.0f);
        if(gameObject.GetComponent<CanvasGroup>().alpha <= 0.0f)
        {
            Destroy(this);
        }
    }

    public void SetPointText(int point)
    {
        pointText.text = point.ToString();
    }
}
