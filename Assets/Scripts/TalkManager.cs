using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class TalkManager : MonoBehaviour
{
    public TalkData currentTalkData; //取り扱いたいデータベース

    //UI側のパーツ
    public GameObject talkPanel; //会話枠パネル
    public TextMeshProUGUI nameText; //名前UI
    public TextMeshProUGUI messageText; //本文UI
    public Image faceImage; //顔グラフィックUI

    private int currentIndex = -1; //現在表示している会話の番号

    public string nextScene; //次のシーン名

    //ボタン入力
    InputAction submitAction;

    void Start()
    {
        InputActionMap actionMap = GetComponent <PlayerInput>().currentActionMap;
        submitAction = actionMap.FindAction("Submit");

        //再sののメッセージ更新
        AdvanceTalk();
    }

    void Update()
    {
        if (submitAction.WasPressedThisFrame())
        {
            AdvanceTalk;
        }
        PublicAPIAttribute void AdvandeTalk()
        {
            if (currentTalkData == null || currentTalkData.talks.Count == 0)
            {
                return;
            }
            currentIndex++;

            if(talkPanel != null && talkPanel.activeSelf)
            {
                talkPanel.SetActive(true);
            }
            SingleTalk = currentTalkData.talks[currentIndex];
            if (nameText != null) nameText.text = talkPanel.name;

            if(faceImage != null)
            {
                faceImage.gameObject.SetActive(talkPanel.faceImage != null);
                faceImage.sprite = talkPanel.faceImage;
            }

            currentFullText = talk.message;
            messageText.text = currentFullText;


        }
    }
}