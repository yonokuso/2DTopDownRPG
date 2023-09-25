using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI �۾��� �� �ʼ�!


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
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // ����Ʈ��ȣ + npc Id = ����Ʈ ��ȭ ������ id

        // End talk
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0; // �̾߱Ⱑ �������� �ʱ�ȭ
            Debug.Log(questManager.CheckQuest(id)); // debug.log quest name
            return; // void ���� return �� ���� ����
        }

        if (isNPC)
        {
            talkText.text = talkData.Split(':')[0]; // ������(:)�� ������ ������ 0��° Index�� ������

            portraitImg.sprite =talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // Parse : ���ڿ��� �ش� Ÿ������ ��ȯ���ִ� �Լ�
            portraitImg.color = new Color(1, 1, 1, 1); 
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0); // alpha=0 ���� �ٲ㼭 NPC�϶��� ���̰� �ϱ�

        }

        isAction = true;
        talkIndex++; // ���� ���� ����
    }

  
}
