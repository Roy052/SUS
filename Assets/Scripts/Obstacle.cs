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
    public Camera mainCamera;

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
                    yield return StartCoroutine(Co_PatternMove(currentData, true, xSpeed));
                    break;
                case MovementType.Appear:
                    currentImage.SetActive(true);
                    yield return new WaitForSeconds(currentData.time / xSpeed);
                    break;
                case MovementType.Spread:
                    StartCoroutine(Co_Spread(currentData, true));
                    yield return new WaitForSeconds(currentData.time / xSpeed);
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
                    yield return StartCoroutine(Co_PatternMove(currentData, false, xSpeed));
                    break;
                case MovementType.Appear:
                    currentImage.SetActive(true);
                    yield return new WaitForSeconds(currentData.time / xSpeed);
                    break;
                case MovementType.Spread:
                    StartCoroutine(Co_Spread(currentData, false));
                    yield return new WaitForSeconds(currentData.time / xSpeed);
                    break;
            }

            currentIdx++;
        }
    }

    IEnumerator Co_PatternMove(Movement data, bool isShadow, float xSpeed)
    {
        if (data.positions.Count < 2)
            yield break;
        
        Image currentImage = childs[data.currentChidIdx];
        currentImage.color = isShadow ? ShadowColor : NaturalColor;
        currentImage.raycastTarget = isShadow == false;
        RectTransform rect = currentImage.rectTransform;
        currentImage.SetActive(true);
        Vector3 posStart = data.positions[0];
        Vector3 posEnd = data.positions[1];
        float moveTime = data.time / xSpeed;
        float time = 0f;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            rect.anchoredPosition = Vector3.Lerp(posStart, posEnd, time / moveTime);
            yield return null;
        }
    }

    private List<GameObject> spreadObjects = new List<GameObject>();
    private List<float> spreadSpeeds = new List<float>();

    IEnumerator Co_Spread(Movement data, bool isShadow)
    {
        yield return null;
        mainCamera = Camera.main;
        Image currentImage = childs[data.currentChidIdx];
        currentImage.color = isShadow ? ShadowColor : NaturalColor;
        currentImage.raycastTarget = isShadow == false;
        RectTransform rect = currentImage.rectTransform;
        int objectCount = data.spreadCount;
        for (int i = 0; i < objectCount; i++)
        {
            // 각도를 등간격으로 계산 (라디안 단위)
            float angle = i * Mathf.PI * 2 / objectCount;

            // 위치 계산 - 중심에서 radius 만큼 떨어진 위치에 배치
            Vector3 position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * data.spreadRadius;

            // 이동 방향 계산 (중심에서 바깥쪽으로)
            Vector3 direction = (position - transform.position).normalized;

            // GameObject 생성
            GameObject obj = Instantiate(currentImage.gameObject, position, Quaternion.identity);
            obj.SetActive(true);

            // 해당 방향으로 회전 (위쪽을 이동 방향으로 설정)
            float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            obj.transform.rotation = Quaternion.Euler(0, 0, degrees);

            // 리스트에 추가
            spreadObjects.Add(obj);
            spreadSpeeds.Add(data.spreadSpeed);
        }
    }

    void Update()
    {
        for (int i = spreadObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = spreadObjects[i];

            // 이동 방향 계산 (중심에서 바깥쪽으로)
            Vector3 direction = (obj.transform.position - transform.position).normalized;

            // 해당 방향으로 이동
            obj.transform.position += direction * spreadSpeeds[i] * Time.deltaTime;

            if (mainCamera == null)
                break;
            // 화면 밖으로 나갔는지 확인
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(obj.transform.position);
            if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                // GameObject 파괴
                Destroy(obj);
                spreadObjects.RemoveAt(i);
                spreadSpeeds.RemoveAt(i);
            }
        }
    }
}
