using UnityEngine;
using System.Collections.Generic;

public enum MovementType
{
    Wait,
    Move,
    Appear,
    Spread,

}

public class ObstacleData : MonoBehaviour
{
    public string obstacleName;
    public List<Movement> movements = new List<Movement>();
}

public class Movement
{
    public MovementType type;
    public int currentSiblingIdx;
    public List<Vector3> positions = new List<Vector3>();
    public float time;
    public List<int> spreadCount = new List<int>();
}
