using UnityEngine;
using UnityEngine.UI;

public class MenuSM : MonoBehaviour
{
    public GameSM gameSM;
    
    public void OnClickGameStart()
    {
        gameSM.SetActive(true);
        gameSM.StartGame();
        gameObject.SetActive(false);
    }
}
