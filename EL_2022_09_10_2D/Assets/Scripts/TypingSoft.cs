using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class TypingSoft : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 3.0f;

    //�@���̓��{�ꕶ
    private string[] qJ = { "���", "�e�X�g", "�^�C�s���O", "���߂��߂����" };
    //�@���̃��[�}����
    private string[] qR = { "monndai", "tesuto", "taipinngu", "kamekumechann" };
    //�@���{��\���e�L�X�g
    private Text UIJ;
    //�@���[�}���\���e�L�X�g
    private Text UIR;
    //�@���{����
    private string nQJ;
    //�@���[�}�����
    private string nQR;
    //�@���ԍ�
    private int numberOfQuestion;
    //�@���̉������ڂ�
    private int indexOfString;

    //�@���͂���������e�L�X�g
    private Text UII;
    //�@����
    private int correctN;
    //�@���𐔕\���p�e�L�X�gUI
    private Text UIcorrectA;
    //�@������������������Ă���
    private string correctString;

    //�@���s��
    private int mistakeN;
    //�@���s���\���p�e�L�X�gUI
    private Text UImistake;

    //�@����
    private float correctAR;
    //�@���𗦕\���p�e�L�X�gUI
    private Text UIcorrectAR;

    // ���t�@�C���p�X
    [SerializeField] private string _textFilePath;

    [SerializeField] private SerihuSpawner _serihuSpawner;

    // ���i�[�ϐ�
    private List<string> _japaneseQuestion = new List<string>();
    private List<string> _romeQuestion = new List<string>();

    // ���Ԍv��
    private float _time = 0.0f;

    private int exclamationMarkCount = 0;

    public bool resetPositionFlag = false;

    public bool stopFlag = false;

    private bool _inputCompleted = false; 

    void Start()
    {
        LoadTextFile();

        //�@�e�L�X�gUI���擾
        UIJ = transform.Find("InputPanel/QuestionJ").GetComponent<Text>();
        UIR = transform.Find("InputPanel/QuestionR").GetComponent<Text>();
        UII = transform.Find("InputPanel/Input").GetComponent<Text>();
        UIcorrectA = transform.Find("DataPanel/Correct Answer").GetComponent<Text>();
        UImistake = transform.Find("DataPanel/Mistake").GetComponent<Text>();
        UIcorrectAR = transform.Find("DataPanel/Correct Answer Rate").GetComponent<Text>();

        //�@�f�[�^����������
        correctN = 0;
        UIcorrectA.text = correctN.ToString();
        mistakeN = 0;
        UImistake.text = mistakeN.ToString();
        correctAR = 0;
        UIcorrectAR.text = correctAR.ToString();

        //�@���o�̓��\�b�h���Ă�
        OutputQ();

        // �ϐ�������
        _time = 0.0f;

        resetPositionFlag = false;

        //stopFlag = false;

        _inputCompleted = false;
    }

    private void LoadTextFile()
    {
        int count = 0;
        // ��s���ǂݍ���
        using (var fs = new StreamReader(Application.dataPath + "/test.txt", System.Text.Encoding.GetEncoding("UTF-8")))
        {
            while (fs.Peek() != -1)
            {
                if(count % 2 == 0)
                {
                    _japaneseQuestion.Add(fs.ReadLine());
                }
                else
                {
                    _romeQuestion.Add(fs.ReadLine());
                }

                count++;
            }

        }
    }

    //�@�V��������\�����郁�\�b�h
    void OutputQ()
    {
        //�@�e�L�X�gUI������������
        UIJ.text = "";
        UIR.text = "";
        UII.text = "";

        //�@���������������������
        correctString = "";
        //�@�����̈ʒu��0�Ԗڂɖ߂�
        indexOfString = 0;

        //�@��萔���Ń����_���ɑI��
        numberOfQuestion = Random.Range(0, _japaneseQuestion.Count);

        //�@�I�����������e�L�X�gUI�ɃZ�b�g
        nQJ = _japaneseQuestion[numberOfQuestion] ;
        nQR = _romeQuestion[numberOfQuestion];

        //�@�I�����������e�L�X�gUI�ɃZ�b�g

        UIJ.text = _japaneseQuestion[numberOfQuestion];
        UIR.text = _romeQuestion[numberOfQuestion];

        // ���Ԃ�߂�
        _time = 0.0f;
        _inputCompleted = false;
        resetPositionFlag = true;
    }

    void Update()
    {
        if(stopFlag == false)
        {
            UIJ.enabled = true;
            UIR.enabled = true;

            _time += Time.deltaTime;
        }
        else
        {
            UIJ.enabled = false;
            UIR.enabled = false;
        }


        if (_inputCompleted == true && _timeLimit > _time)
        {
            ExclamationMarkCount();

            return;
        }
        else if(_inputCompleted == true && _timeLimit <= _time)
        {
            var serihu = _japaneseQuestion[numberOfQuestion];
            for (int i = 0; i < exclamationMarkCount; i++)
            {
                serihu += "!";
            }

            exclamationMarkCount = 0;

            // �Z���t����
            _serihuSpawner.Spawn(serihu);

            OutputQ();

            Debug.Log(_time);

            stopFlag = true;

            return;
        }
        //if (stopFlag == true)
        //{

        //    UIJ.enabled = false;
        //    UIR.enabled = false;
        //    return;
        //}

        //�@�����Ă��镶���ƃL�[�{�[�h����ł����������������ǂ���
        if (Input.GetKeyDown(nQR[indexOfString].ToString()))
        {
            //�@�������̏������Ăяo��
            Correct();

                //�@������͂��I�����玟�̖���\��
            if (indexOfString >= nQR.Length)
            {
                _inputCompleted = true;

                //// �Z���t����
                //_serihuSpawner.Spawn(_japaneseQuestion[numberOfQuestion]);

                //OutputQ();

                //stopFlag = true;
            }
        }
        else if (Input.anyKeyDown)
        {
            //�@���s���̏������Ăяo��
            Mistake();
        }

        // ����
        MesureTime();
    }

    //�@�^�C�s���O�������̏���
    void Correct()
    {
        //�@���𐔂𑝂₷
        correctN++;
        UIcorrectA.text = correctN.ToString();
        //�@���𗦂̌v�Z
        CorrectAnswerRate();
        //�@��������������\��
        correctString += nQR[indexOfString].ToString();
        UII.text = correctString;
        //�@���̕������w��
        indexOfString++;
    }

    //�@�^�C�s���O���s���̏���
    void Mistake()
    {
        //�@���s���𑝂₷�i���������ɂ��Ή�������j
        mistakeN += Input.inputString.Length;

        UImistake.text = mistakeN.ToString();
        //�@���𗦂̌v�Z
        CorrectAnswerRate();
        //�@���s����������\��
        if (Input.inputString != "")
        {
            UII.text = correctString + "<color=#ff0000ff>" + Input.inputString + "</color>";
        }
    }

    //�@���𗦂̌v�Z����
    void CorrectAnswerRate()
    {
        //�@���𗦂̌v�Z
        correctAR = 100f * correctN / (correctN + mistakeN);
        //�@�����_�ȉ��̌������킹��
        UIcorrectAR.text = correctAR.ToString("0.00");
    }

    void MesureTime()
    {
        // �������Ԃ��߂�����
        if(_timeLimit < _time)
        {
            //���̕������o��
            OutputQ();
            Debug.Log(_time);
        }
    }

    public float GetTimeLimit() { return _timeLimit; }

    void ExclamationMarkCount()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            correctString += "!";
            UII.text = correctString;
            exclamationMarkCount++;
        }
    }
}
