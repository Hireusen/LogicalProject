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
    public float[] size;
    public Transform[] tr;
    // 아이템
    public GameObject[] itemGO;
    public Vector2[] itemPos;
    public int activeItemCount;
    // 시스템
    public int itemSpawnCount;
    public Vector2 cameraMinPos;
    public Vector2 cameraMaxPos;

    // 대부분의 변수 별도 초기화 작업 필요
    public WormData(int length, Vector2 headDir)
    {
        this.length = length;
        this.headDir = headDir;
        this.pos = new Vector2[length];
        this.size = new float[length];
        this.tr = new Transform[length];
        this.itemGO = null;
        this.activeItemCount = 0;
        this.itemSpawnCount = 0;
        this.cameraMinPos = Vector2.zero;
        this.cameraMinPos = Vector2.zero;
    }
}
