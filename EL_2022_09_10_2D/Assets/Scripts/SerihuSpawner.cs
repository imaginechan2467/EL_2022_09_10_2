using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerihuSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private Vector3 _spawnPosition;

    public void Spawn(string serihu)
    {
        GameObject gameObject = Instantiate<GameObject>(_prefab, canvas);
        RawImage image = gameObject.GetComponent<RawImage>();
        image.rectTransform.position = _spawnPosition + canvas.transform.position;
        Text text = gameObject.transform.GetChild(0).GetComponent<Text>();
        text.text = serihu;
    }
}
