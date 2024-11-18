using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSM : MonoBehaviour
{
    const float MaxSecond = 20f;

    public Text textGameStatus;
    public List<Obstacle> obstacles;

    public GameObject objFail;
    public GameObject objSuccess;
    public GameObject objEnd;

    bool isGameEnd = false;
    private void Start()
    {
        objEnd.SetActive(false);
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
            if (idx >= randomIdxList.Count)
                break;

            int currentIdx = randomIdxList[idx];
            obstacles[currentIdx].Set(DataManager.obstacleDatas[currentIdx]);

            float currentTime = obstacles[currentIdx].GetShowTime(xSpeed);
            if (time + currentTime <= MaxSecond)
            {
                idx++;
                obstacles[currentIdx].Show(xSpeed);
                time += currentTime;
                yield return new WaitForSeconds(currentTime);
                continue;
            }    
            yield return null;
            currentIdx++;

            if (isGameEnd)
                yield break;
        }
    }

    public void GameFail()
    {
        isGameEnd = true;
        Debug.Log(isGameEnd);
        objEnd.SetActive(true);
        objFail.SetActive(true);
        objSuccess.SetActive(false);
    }
}
