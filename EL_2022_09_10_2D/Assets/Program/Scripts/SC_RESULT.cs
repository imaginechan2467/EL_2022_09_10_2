using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SC_RESULT : MonoBehaviour
{
    [SerializeField] List<SC_BUTTON> buttonList = new List<SC_BUTTON>();
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject BackImage;
    [SerializeField] GameObject scoreObject;
    [SerializeField] GameObject donObject;

    int currentCursor = 0;
    const float CURSOR_TIME = 0.5f;
    float cursorTime = 0.0f;    // 次の入力を受け付けるまでの時間
    bool isCursorPush = false;
    bool isScorePop = false;
    bool isDonPop = false;
    float scorePopSpeed = 1.5f;
    float donPopSpeed = 15.0f;

    float tempScore = 1000000.0f;


    void Start()
    {
        scoreObject.transform.localScale = new Vector3(0, 0, 0);
        scoreObject.GetComponent<CanvasGroup>().alpha = 0;
        donObject.transform.localScale = new Vector3(10, 10, 10);
        donObject.GetComponent<CanvasGroup>().alpha = 0;
        scoreText.text = "SCORE : " + tempScore.ToString();
    }

    void Update()
    {
        // スコアオブジェクトの更新
        scoreObject.transform.localScale = new Vector3(
            Mathf.Min(scoreObject.transform.localScale.x + scorePopSpeed * Time.deltaTime, 1.0f),
            Mathf.Min(scoreObject.transform.localScale.y + scorePopSpeed * Time.deltaTime, 1.0f),
            Mathf.Min(scoreObject.transform.localScale.z + scorePopSpeed * Time.deltaTime, 1.0f));
        scoreObject.GetComponent<CanvasGroup>().alpha = Mathf.Min(scoreObject.GetComponent<CanvasGroup>().alpha + scorePopSpeed * Time.deltaTime, 1.0f);
        if (scoreObject.transform.localScale.x >= 1.0f)
            isScorePop = true;

        // ドン！オブジェクトの更新
        if (!isScorePop)
            return;
        donObject.transform.localScale = new Vector3(
            Mathf.Max(donObject.transform.localScale.x - donPopSpeed * Time.deltaTime, 1.5f),
            Mathf.Max(donObject.transform.localScale.y - donPopSpeed * Time.deltaTime, 1.5f),
            Mathf.Max(donObject.transform.localScale.z - donPopSpeed * Time.deltaTime, 1.5f));
        donObject.GetComponent<CanvasGroup>().alpha = Mathf.Min(donObject.GetComponent<CanvasGroup>().alpha + donPopSpeed * Time.deltaTime, 1.0f);
        if (donObject.transform.localScale.x <= 1.5f)
        {
            isDonPop = true;
            scoreObject.GetComponent<SC_SWAY>().Play();
            donObject.GetComponent<SC_SWAY>().Play();
            BackImage.GetComponent<SC_SWAY>().Play();
            buttonList[0].GetComponent<SC_SWAY>().Play();
            buttonList[1].GetComponent<SC_SWAY>().Play();
            buttonList[2].GetComponent<SC_SWAY>().Play();
        }


        if (!isScorePop || !isDonPop)
            return;

        cursorTime = Mathf.Max(cursorTime - Time.deltaTime, 0.0f);

        float key = Input.GetAxisRaw("Horizontal");
        if(key != 0 && cursorTime <= 0.0f)
        {
            if (key > 0)
            {
                if(!isCursorPush)
                {
                    currentCursor = (currentCursor + 1) % buttonList.Count;
                    cursorTime = CURSOR_TIME;
                    isCursorPush = true;
                }
                else if(isCursorPush)
                {
                    currentCursor = (currentCursor + 1) % buttonList.Count;
                    cursorTime = CURSOR_TIME / 5;
                }

            }
            else if (key < 0)
            {
                if (!isCursorPush)
                {
                    currentCursor = (currentCursor + buttonList.Count - 1) % buttonList.Count;
                    cursorTime = CURSOR_TIME;
                    isCursorPush = true;
                }
                else if (isCursorPush)
                {
                    currentCursor = (currentCursor + buttonList.Count - 1) % buttonList.Count;
                    cursorTime = CURSOR_TIME / 5;
                }
            }
        }
        else if(key == 0.0f)
        {
            cursorTime = 0.0f;
            isCursorPush = false;
        }

        // ボタンの更新
        for(int i = 0; i < buttonList.Count; i++)
        {
            bool select;
            if (i == currentCursor)
                select = true;
            else
                select = false;

            buttonList[i].SetIsSelect(select);

            buttonList[i].ButtonUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            string nextScene = "";
            switch (currentCursor)
            {
                case 0:
                    nextScene = "ResultScene";
                    break;
                case 1:
                    nextScene = "GameScene";
                    break;
                case 2:
                    nextScene = "Exit";
                    break;
                default:
                    break;
            }
            SC_MG_FADE.Instance.ChangeScene(nextScene, 1.0f, 1.0f);
        }
    }
}
