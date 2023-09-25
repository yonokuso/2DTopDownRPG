using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject; // 퀘스트 오브젝트 저장
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        // int에 해당 퀘스트와 연결된 npc id를 입력
        questList.Add(10, new QuestData("양배추 요리를 만들기.", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("양배추 요리를 가져다주기.", new int[] { 300, 200, 2000 }));
        questList.Add(30, new QuestData("새로운 레시피 실험하기.", new int[] { 1000, 300, 600, 200 })); // 한 퀘스트에 같은 npc가 두번 들어가는 건 안되는 듯?
        questList.Add(40, new QuestData("숲 탐험하기.", new int[] { 1000 }));
        questList.Add(50, new QuestData("모든 퀘스트 완료!", new int[] { 0 })); // 더미 데이터


    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id) // 대화 진행을 위해 퀘스트 대화순서++
    {
        // Next Talk Target
        if (id == questList[questId].npcId[questActionIndex]) // 왜 같다고 하는거지
            questActionIndex++; // npc와 대화할 때만 증가? -> 아님!! 오브젝트랑 대화할때도 증가함

        // Control Quest Object
        ControlObject();

        // Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // Quest Nmae
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:                
                break;
            case 20:
                if (questActionIndex == 3)
                    questObject[0].SetActive(true);
                break;
            case 30:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
            case 40:
                if (questActionIndex == 1)
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(true);
                break;
        }
    }
}
