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
        },
        //Bomb
        new ObstacleData()
        {
            movements = new List<Movement>()
            {
                new Movement()
                {
                    type = MovementType.Move,
                    currentChidIdx = 0,
                    positions = new List<Vector3>(){ new Vector3(-760, -540), new Vector3(-760, 160),},
                    isEndRandomY = true,
                    time = 1f,
                },
                new Movement()
                {
                    type = MovementType.Move,
                    currentChidIdx = 1,
                    positions = new List<Vector3>(){ new Vector3(0, -540), new Vector3(0, 0),},
                    isEndRandomY = true,
                    time = 1f,
                },
                new Movement()
                {
                    type = MovementType.Move,
                    currentChidIdx = 2,
                    positions = new List<Vector3>(){ new Vector3(760, -540), new Vector3(760, -160),},
                    isEndRandomY = true,
                    time = 1f,
                },
                new Movement()
                {
                    type = MovementType.Explode,
                    currentChidIdx = 0,
                    time = 0.5f,
                },
                new Movement()
                {
                    type = MovementType.Explode,
                    currentChidIdx = 1,
                    time = 0.5f,
                },
                new Movement()
                {
                    type = MovementType.Explode,
                    currentChidIdx = 2,
                    time = 0.5f,
                }
            }
        },
        //Pheonix
        new ObstacleData()
        {
            movements = new List<Movement>()
            {
                new Movement()
                {
                    type = MovementType.Move,
                    currentChidIdx = 0,
                    positions = new List<Vector3>(){ new Vector3(960f, 0), new Vector3(-960f, 0)},
                    time = 2f,
                },
                new Movement()
                {
                    type = MovementType.Wait,
                    time = 0.5f
                },
                new Movement()
                {
                    type = MovementType.Appear,
                    currentChidIdx = 1,
                    time = 0.5f,
                },
            }
        }
        
    };
}
