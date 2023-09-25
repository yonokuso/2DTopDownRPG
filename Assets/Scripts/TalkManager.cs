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
        // 대화 데이터 입력 추가
        // 구분자(:)와 초상화 Index를 문장 뒤에 추가해 문장마다 다른 초상화 할당.
        // NPC A: 1000 ,  NPC B: 2000
        // Box: 100 , Desk: 200 , Cabbage: 300 , Heart: 500 , Water: 600 , Rock: 700
        talkData.Add(1000, new string[] { "안녕?:0", "여기까지 해냈네.:1" });
        talkData.Add(2000, new string[] { "집에 가고 싶다.:0","너는 어때?:1" });


        talkData.Add(100, new string[] { "뭔가 위에 올라가고 싶어지는 나무 상자다." });
        talkData.Add(200, new string[] { "요리를 할 수 있는 테이블이다." });
        talkData.Add(300, new string[] { "신선해보이는 양배추다." });
        talkData.Add(400, new string[] { "평범한 집이다." });
        talkData.Add(600, new string[] { "푸른 호수다." });
        talkData.Add(700, new string[] { "길이 막혀있다." });



        // Quest Talk (questActionIndex?)
        talkData.Add(10 + 1000, new string[] { "안녕. 어서 와.:0", "지금부터 양배추를 캐는 법을 알려줄게.:0", "저기 위에 밭 보이지? 저기서 양배추를 수확할 수 있어.:1", "요리하는 법은 그 근처의 주민에게 물어보면 돼.:1", "보답으로 요리 가져다주는 거 잊지 마!:2" });
        talkData.Add(11 + 2000, new string[] { "아, 요리? 중앙 구역 테이블에서 만들 수 있어.:1", "그럼 잘 해봐.:0" }); // questactionindex++로 1이 증가

        talkData.Add(20 + 300, new string[] { "이곳에서 양배추를 수확할 수 있다.", ".", "..", "...", "양배추 1개를(을) 수확했다!" });
        talkData.Add(21 + 200, new string[] { "이곳에서 요리를 할 수 있다.", ".", "..", "...", "양배추 볶음을 완성했다!"});
        talkData.Add(22 + 2000, new string[] { "양배추 볶음이네. 어디 먹어볼까.:0", "...:1", "음.:2", "처음치고 나쁘지 않은 걸? 잘 먹었어.:2", "[주민을 만족시켰다!]:2", "다른 요리도 만들어 보는 건 어때? 보답으로 레시피를 줄게.:2", "[양배추 롤 레시피를 얻었다!]:2" });
        
        talkData.Add(30 + 1000, new string[] { "요리에 성공했구나! 축하해!:0", "양배추 롤은 삶아야 하니까 강에서 물을 길어오는 게 좋아.:1" });
        talkData.Add(31 + 300, new string[] { "이곳에서 양배추를 수확할 수 있다.", ".", "..", "...", "양배추 1개를(을) 수확했다!" });
        talkData.Add(32 + 600, new string[] { "이곳에서 물을 길 수 있다.", ".", "..", "...", "물을 길었다.", "양배추 롤을 만들 준비가 되었다!" });
        talkData.Add(33 + 200, new string[] { "이곳에서 요리를 할 수 있다.", ".", "..", "...", "양배추 롤을 완성했다!" });
        
        talkData.Add(40 + 1000, new string[] { "이제 제법 준비가 된 것 같네?:1", "좋아. 새로운 곳에 갈 수 있게 해줄게.:0", "더 다양한 요리를 만들 수 있을거야.:0", "[숲에 갈 수 있게 되었다!]:0", "더 맛있는 양배추 요리 기대할게!:2" });




        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);


    }


    // id: 대화 가져오기
    // talkIndex: 문자열을 배열로 했기 때문에 몇번째 문장을 가져올 것인지.
    public string GetTalk(int id, int talkIndex) 
    {
        // 예외 처리
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                // 퀘스트 맨 처음 대사마저 없을 때,
                // 기본 대사를 가지고 온다.
                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else
            {
                // 해당 퀘스트 진행 순서 대사가 없을 때
                // 퀘스트 맨 처음 대사를 가지고 온다.
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }
        }


        if (talkIndex == talkData[id].Length)
            return null; // 남아있는 문장이 없음
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex]; // 위랑 같은 구조
    }
    
}
