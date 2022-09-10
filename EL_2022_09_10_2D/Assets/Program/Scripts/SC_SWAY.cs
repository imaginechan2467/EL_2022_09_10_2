using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SWAY : MonoBehaviour
{
    Vector3 startPosition;
    Vector2 move;
    float sign = 1.0f;
    [SerializeField] float amplitude;
    bool isPlay = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isPlay)
            return;

        move.x = Random.value * amplitude;
        move.y = Random.value * amplitude;
        transform.position = new Vector3(startPosition.x + move.x * sign, startPosition.y + move.y * sign, 0.0f);
        sign *= -1.0f;
    }

    public void Play()
    {
        isPlay = true;
    }
}
