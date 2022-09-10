using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager score_manager;

    [SerializeField]
    private TextMeshProUGUI score_text;
    private int score = 0;

    void Awake()
    {
        //singlton
        if (score_manager == null)
        {
            score_manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "SCORE : " + score.ToString("d6");
    }

    public void AddScore(string word)
    {
        int add = 100 + (10 * GetWordNum(word));
        score += add;
    }

    int GetWordNum(string original)
    {
        //string search = "!";
        //string tmp = original.Replace(search, "");
        //return (original.Length - tmp.Length) / search.Length;

        int count = 0;
        string search1 = "!";
        string tmp1 = original.Replace(search1, "");
        if(tmp1.Length > 0)
        {
            count += (original.Length - tmp1.Length) / search1.Length;
        }

        string search2 = "I";
        string tmp2 = original.Replace(search2, "");
        if(tmp2.Length > 0)
        {
            count += (original.Length - tmp2.Length) / search2.Length;
        }
        return count;
    }
}
