using UnityEngine;

/// <summary>
/// 지렁이 시뮬레이터에 관한 정보를 담는 클래스입니다.
/// </summary>
public class WormData
{
    public int length;
    public Vector2 headDir;
    public Vector2[] curPos;
    public float[] size;
    public Transform[] tr;
    // System
    public int itemSpawnCount;
    public Vector2 cameraMinPos;
    public Vector2 cameraMaxPos;

    public WormData(int length, Vector2 headDir)
    {
        this.length = length;
        this.headDir = headDir;
        this.curPos = new Vector2[length];
        this.size = new float[length];
        this.tr = new Transform[length];
        this.itemSpawnCount = 0;
    }
}
