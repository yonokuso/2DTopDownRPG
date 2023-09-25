using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 작업할 때 필수!


public class GameManager: MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Image portraitImg;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    public void Action(GameObject scanObj)
    { 
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction);

    }

    // Start is called before the first frame update
    void Talk(int id, bool isNPC)
    {
        // Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // 퀘스트번호 + npc Id = 퀘스트 대화 데이터 id

        // End talk
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0; // 이야기가 끝났으니 초기화
            Debug.Log(questManager.CheckQuest(id)); // debug.log quest name
            return; // void 에서 return 은 강제 종료
        }

        if (isNPC)
        {
            talkText.text = talkData.Split(':')[0]; // 구분자(:)로 문장을 나누고 0번째 Index를 가져옴

            portraitImg.sprite =talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // Parse : 문자열을 해당 타입으로 변환해주는 함수
            portraitImg.color = new Color(1, 1, 1, 1); 
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0); // alpha=0 으로 바꿔서 NPC일때만 보이게 하기

        }

        isAction = true;
        talkIndex++; // 다음 문장 진행
    }

  
}
