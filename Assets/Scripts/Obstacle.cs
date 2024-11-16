using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    const float WaitTime = 0.5f;
    static readonly Color ShadowColor = new Color(0, 0, 0, 0.5f);
    static readonly Color NaturalColor = new Color(1f, 1f, 1f, 1f);

    public RectTransform rect;
    public ObstacleData data;

    public float GetShowTime(float xSpeed)
    {
        float time = 0f;
        foreach(var movement in data.movements)
            time += movement.time / xSpeed;
        return time;
    }

    public void Set(ObstacleData data)
    {
        this.data = data;
    }

    public void Show(float xSpeed)
    {
        StartCoroutine(Co_Show(data, xSpeed));
    }

    IEnumerator Co_Show(ObstacleData data, float xSpeed)
    {
        int currentIdx = 0;

        //Shadow
        while (currentIdx < data.movements.Count)
        {
            float time = 0;
            var currentData = data.movements[currentIdx];
            Image currentImage = transform.GetChild(currentData.currentSiblingIdx).GetComponent<Image>();
            currentImage.color = ShadowColor;
            currentImage.raycastTarget = false;

            switch (data.movements[currentIdx].type)
            {
                case MovementType.Wait:
                    yield return new WaitForSeconds(currentData.time / xSpeed);
                    break;
                case MovementType.Move:
                    if (currentData.positions.Count < 2)
                        break;

                    Vector3 posStart = currentData.positions[0];
                    Vector3 posEnd = currentData.positions[1];
                    float moveTime = currentData.time / xSpeed;
                    while (time < moveTime)
                    {
                        time += Time.deltaTime;
                        rect.anchoredPosition = Vector3.Lerp(posStart, posEnd, time / moveTime);
                        yield return null;
                    }
                    break;
                case MovementType.Appear:

                    break;
                case MovementType.Spread:
                    break;
            }

            currentIdx++;
        }

        yield return new WaitForSeconds(WaitTime / xSpeed);

        currentIdx = 0;
        //Real
        while (currentIdx < data.movements.Count)
        {
            float time = 0;
            var currentData = data.movements[currentIdx];
            Image currentImage = transform.GetChild(currentData.currentSiblingIdx).GetComponent<Image>();
            currentImage.color = NaturalColor;
            currentImage.raycastTarget = true;

            switch (data.movements[currentIdx].type)
            {
                case MovementType.Wait:
                    yield return new WaitForSeconds(currentData.time / xSpeed);
                    break;
                case MovementType.Move:
                    if (currentData.positions.Count < 2)
                        break;

                    currentImage.SetActive(true);
                    Vector3 posStart = currentData.positions[0];
                    Vector3 posEnd = currentData.positions[1];
                    float moveTime = currentData.time / xSpeed;
                    while (time < moveTime)
                    {
                        time += Time.deltaTime;
                        rect.anchoredPosition = Vector3.Lerp(posStart, posEnd, time / moveTime);
                        yield return null;
                    }
                    break;
                case MovementType.Appear:
                    currentImage.SetActive(true);
                    yield return new WaitForSeconds(currentData.time / xSpeed);
                    break;
                case MovementType.Spread:
                    break;
            }

            currentIdx++;
        }
    }
}
