using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        GenerateData();
    }

    void GenerateData()
    {
        // ��ȭ ������ �Է� �߰�
        // ������(:)�� �ʻ�ȭ Index�� ���� �ڿ� �߰��� ���帶�� �ٸ� �ʻ�ȭ �Ҵ�.
        // NPC A: 1000 ,  NPC B: 2000
        // Box: 100 , Desk: 200 , Cabbage: 300 , Heart: 500 , Water: 600 , Rock: 700
        talkData.Add(1000, new string[] { "�ȳ�?:0", "������� �س³�.:1" });
        talkData.Add(2000, new string[] { "���� ���� �ʹ�.:0","�ʴ� �?:1" });


        talkData.Add(100, new string[] { "���� ���� �ö󰡰� �;����� ���� ���ڴ�." });
        talkData.Add(200, new string[] { "�丮�� �� �� �ִ� ���̺��̴�." });
        talkData.Add(300, new string[] { "�ż��غ��̴� ����ߴ�." });
        talkData.Add(400, new string[] { "����� ���̴�." });
        talkData.Add(600, new string[] { "Ǫ�� ȣ����." });
        talkData.Add(700, new string[] { "���� �����ִ�." });



        // Quest Talk (questActionIndex?)
        talkData.Add(10 + 1000, new string[] { "�ȳ�. � ��.:0", "���ݺ��� ����߸� ĳ�� ���� �˷��ٰ�.:0", "���� ���� �� ������? ���⼭ ����߸� ��Ȯ�� �� �־�.:1", "�丮�ϴ� ���� �� ��ó�� �ֹο��� ����� ��.:1", "�������� �丮 �������ִ� �� ���� ��!:2" });
        talkData.Add(11 + 2000, new string[] { "��, �丮? �߾� ���� ���̺��� ���� �� �־�.:1", "�׷� �� �غ�.:0" }); // questactionindex++�� 1�� ����

        talkData.Add(20 + 300, new string[] { "�̰����� ����߸� ��Ȯ�� �� �ִ�.", ".", "..", "...", "����� 1����(��) ��Ȯ�ߴ�!" });
        talkData.Add(21 + 200, new string[] { "�̰����� �丮�� �� �� �ִ�.", ".", "..", "...", "����� ������ �ϼ��ߴ�!"});
        talkData.Add(22 + 2000, new string[] { "����� �����̳�. ��� �Ծ��.:0", "...:1", "��.:2", "ó��ġ�� ������ ���� ��? �� �Ծ���.:2", "[�ֹ��� �������״�!]:2", "�ٸ� �丮�� ����� ���� �� �? �������� �����Ǹ� �ٰ�.:2", "[����� �� �����Ǹ� �����!]:2" });
        
        talkData.Add(30 + 1000, new string[] { "�丮�� �����߱���! ������!:0", "����� ���� ��ƾ� �ϴϱ� ������ ���� ������ �� ����.:1" });
        talkData.Add(31 + 300, new string[] { "�̰����� ����߸� ��Ȯ�� �� �ִ�.", ".", "..", "...", "����� 1����(��) ��Ȯ�ߴ�!" });
        talkData.Add(32 + 600, new string[] { "�̰����� ���� �� �� �ִ�.", ".", "..", "...", "���� �����.", "����� ���� ���� �غ� �Ǿ���!" });
        talkData.Add(33 + 200, new string[] { "�̰����� �丮�� �� �� �ִ�.", ".", "..", "...", "����� ���� �ϼ��ߴ�!" });
        
        talkData.Add(40 + 1000, new string[] { "���� ���� �غ� �� �� ����?:1", "����. ���ο� ���� �� �� �ְ� ���ٰ�.:0", "�� �پ��� �丮�� ���� �� �����ž�.:0", "[���� �� �� �ְ� �Ǿ���!]:0", "�� ���ִ� ����� �丮 ����Ұ�!:2" });




        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);


    }


    // id: ��ȭ ��������
    // talkIndex: ���ڿ��� �迭�� �߱� ������ ���° ������ ������ ������.
    public string GetTalk(int id, int talkIndex) 
    {
        // ���� ó��
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                // ����Ʈ �� ó�� ��縶�� ���� ��,
                // �⺻ ��縦 ������ �´�.
                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else
            {
                // �ش� ����Ʈ ���� ���� ��簡 ���� ��
                // ����Ʈ �� ó�� ��縦 ������ �´�.
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }
        }


        if (talkIndex == talkData[id].Length)
            return null; // �����ִ� ������ ����
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex]; // ���� ���� ����
    }
    
}
