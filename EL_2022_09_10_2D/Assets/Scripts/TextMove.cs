using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMove : MonoBehaviour
{
    [SerializeField] private Text _questionJapanese;
    [SerializeField] private Text _questionRome;

    [SerializeField] private float _offset = 170.0f;

    [SerializeField] private TypingSoft _typingSoft;

    private Vector3 defaultJapanesePosition;

    // Start is called before the first frame update
    void Start()
    {
        defaultJapanesePosition = _questionJapanese.rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_typingSoft.stopFlag == true) return;

        if (_typingSoft.resetPositionFlag == true)
        {
            SetDefaultPosition();
            _typingSoft.resetPositionFlag = false;
        }

        var nowPosition = _questionJapanese.rectTransform.position;
        nowPosition.x += 2.0f;
        _questionJapanese.rectTransform.position = nowPosition;
        nowPosition.y -= _offset;
        _questionRome.rectTransform.position = nowPosition;
    }

    void SetDefaultPosition()
    {
        var defaultPosition = defaultJapanesePosition;
        _questionJapanese.rectTransform.position = defaultPosition;
        defaultPosition.y -= _offset;
        _questionRome.rectTransform.position = defaultPosition;
    }
}
