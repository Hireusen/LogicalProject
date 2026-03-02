using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 지렁이 시뮬레이터에 관한 정보를 담는 클래스입니다.
/// </summary>
public class WormData
{
    // 지렁이
    public int length;
    public Vector2 headDir;
    public Vector2[] pos;
    public Queue<Vector2>[] prevPos;
    public float[] size;
    public Transform[] tr;
    // 아이템
    public GameObject[] itemGO;
    public Vector2[] itemPos;
    public int[] itemNumber;
    public int activeItemCount;
    // 시스템
    public int itemSpawnCount;
    public Vector2 cameraMinPos;
    public Vector2 cameraMaxPos;

    // 대부분의 변수 별도 초기화 작업 필요
    public WormData()
    {
        // WormSpawner Init
        this.length = 0;
        this.headDir = Vector2.zero;
        this.pos = null;
        this.prevPos = null;
        this.size = null;
        this.tr = null;
        // WormItemSpawner Init
        this.itemGO = null;
        this.itemPos = null;
        this.itemNumber = null;
        this.activeItemCount = 0;
        // System
        this.itemSpawnCount = 0;
        this.cameraMinPos = Vector2.zero;
        this.cameraMaxPos = Vector2.zero;
    }
}
