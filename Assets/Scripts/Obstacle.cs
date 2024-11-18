using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour, IPointerEnterHandler
{
    const float WaitTime = 1f;
    static readonly Color ShadowColor = new Color(0, 0, 0, 0.5f);
    static readonly Color NaturalColor = new Color(1f, 1f, 1f, 1f);

    public GameSM gameSM;
    public ObstacleData data;

    public List<Image> childs;

    public float GetShowTime(float xSpeed)
    {
        float time = 0f;
        foreach (var movement in data.movements)
            time += movement.time;

        return (time * 2 + WaitTime) / xSpeed;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameSM.GameFail();
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
            Image currentImage = childs[currentData.currentChidIdx];
            currentImage.color = ShadowColor;
            currentImage.raycastTarget = false;
            RectTransform rect = currentImage.rectTransform;

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

        foreach (var img in childs)
            img.SetActive(false);

        yield return new WaitForSeconds(WaitTime / xSpeed);

        currentIdx = 0;
        //Real
        while (currentIdx < data.movements.Count)
        {
            float time = 0;
            var currentData = data.movements[currentIdx];
            Image currentImage = childs[currentData.currentChidIdx];
            currentImage.color = NaturalColor;
            currentImage.raycastTarget = true;
            RectTransform rect = currentImage.rectTransform;

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
