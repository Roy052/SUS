using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{

    public static List<ObstacleData> obstacleDatas = new List<ObstacleData>()
    {
        //
        new ObstacleData()
        {
            movements = new List<Movement>()
            {
                new Movement()
                {
                    type = MovementType.Wait,
                    time = 0.5f
                },
                new Movement()
                {
                    type = MovementType.Move,
                    positions = new List<Vector3>(){ new Vector3(649, 0), new Vector3(-649, 0) },
                    time = 1f
                },
                new Movement()
                {
                    type = MovementType.Wait,
                    time = 0.5f
                },
            }
        },
        new ObstacleData()
        {
            movements = new List<Movement>()
            {
                new Movement()
                {
                    type = MovementType.Spread,
                    currentChidIdx = 0,
                    positions = new List<Vector3>(){ new Vector3(-480f, 270f) },
                    time = 0.5f,
                    spreadCount = 8,
                    spreadRadius = 2f,
                    spreadSpeed = 15f,
                },
                new Movement()
                {
                    type = MovementType.Wait,
                    time = 0.5f
                },
                new Movement()
                {
                    type = MovementType.Spread,
                    currentChidIdx = 0,
                    positions = new List<Vector3>(){ new Vector3(480f, 0) },
                    time = 0.5f,
                    spreadCount = 8,
                    spreadRadius = 2f,
                    spreadSpeed = 15f,
                },
                new Movement()
                {
                    type = MovementType.Wait,
                    time = 0.5f
                },
                new Movement()
                {
                    type = MovementType.Spread,
                    currentChidIdx = 0,
                    positions = new List<Vector3>(){ new Vector3(0, -270f) },
                    time = 0.5f,
                    spreadCount = 8,
                    spreadRadius = 2f,
                    spreadSpeed = 15f,
                },
                new Movement()
                {
                    type = MovementType.Wait,
                    time = 0.5f
                },
            }
        }
        
    };
}
