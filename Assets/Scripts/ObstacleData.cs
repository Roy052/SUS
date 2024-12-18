using UnityEngine;
using System.Collections.Generic;

public enum MovementType
{
    Disable,
    Wait,
    Move,
    Appear,
    Spread,
    Explode,
    Chase,
}

public class ObstacleData : MonoBehaviour
{
    public string obstacleName;
    public List<Movement> movements = new List<Movement>();
}

public class Movement
{
    public MovementType type;
    public int currentChidIdx;
    public List<Vector3> positions = new List<Vector3>();
    public bool isEndRandomX;
    public bool isEndRandomY;
    public float time;
    public int spreadCount;
    public float spreadRadius;
    public float spreadSpeed;
    public SFXType sfxType;
}
