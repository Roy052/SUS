using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSM : MonoBehaviour
{
    const float MaxSecond = 20f;

    public DataManager dataManager;
    public Text textGameStatus;
    public List<Obstacle> obstacles;

    private void Start()
    {

        StartCoroutine(Co_Game());

    }

    IEnumerator Co_Game()
    {
        float xSpeed = 1f;
        float time = 0f;

        System.Random rand = new System.Random();
        List<int> randomIdxList = Enumerable.Range(0, obstacles.Count).ToList().OrderBy(_ => rand.Next()).ToList();

        int idx = 0;
        while (time <= MaxSecond)
        {
            int currentIdx = randomIdxList[idx];
            obstacles[currentIdx].Set(dataManager.obstacleDatas[currentIdx]);
            if(time + obstacles[currentIdx].GetShowTime(xSpeed) > MaxSecond)
            {
                idx++;
                continue;
            }    
            yield return null;
            currentIdx++;
        }
    }
}
