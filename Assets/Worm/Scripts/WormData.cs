using UnityEngine;

/// <summary>
/// 지렁이 시뮬레이터에 관한 정보를 담는 클래스입니다.
/// </summary>
public class WormData
{
    public int length;
    public Vector2 headDir;
    public Vector2[] curPos;
    public Vector2[] prevPos;
    public float[] size;
    public Transform[] tr;
}
