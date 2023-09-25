using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject; // ����Ʈ ������Ʈ ����
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        // int�� �ش� ����Ʈ�� ����� npc id�� �Է�
        questList.Add(10, new QuestData("����� �丮�� �����.", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("����� �丮�� �������ֱ�.", new int[] { 300, 200, 2000 }));
        questList.Add(30, new QuestData("���ο� ������ �����ϱ�.", new int[] { 1000, 300, 600, 200 })); // �� ����Ʈ�� ���� npc�� �ι� ���� �� �ȵǴ� ��?
        questList.Add(40, new QuestData("�� Ž���ϱ�.", new int[] { 1000 }));
        questList.Add(50, new QuestData("��� ����Ʈ �Ϸ�!", new int[] { 0 })); // ���� ������


    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id) // ��ȭ ������ ���� ����Ʈ ��ȭ����++
    {
        // Next Talk Target
        if (id == questList[questId].npcId[questActionIndex]) // �� ���ٰ� �ϴ°���
            questActionIndex++; // npc�� ��ȭ�� ���� ����? -> �ƴ�!! ������Ʈ�� ��ȭ�Ҷ��� ������

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
