using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerihuMove : MonoBehaviour
{
    private TypingSoft _typingSoft;

    private RawImage _rawIamge;

    private void Awake()
    {
        _typingSoft = FindObjectOfType<TypingSoft>();
        _rawIamge = GetComponent<RawImage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var nowPosition = _rawIamge.rectTransform.position;
        nowPosition.x -= 3.0f;
        _rawIamge.rectTransform.position = nowPosition;

        if(_rawIamge.rectTransform.position.x + (_rawIamge.rectTransform.sizeDelta.x / 2.0f) < 0.0f)
        {
            _typingSoft.stopFlag = false;
            Destroy(this.gameObject);
        }
    }
}
