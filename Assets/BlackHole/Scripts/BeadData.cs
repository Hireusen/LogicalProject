using UnityEngine;

/// <summary>
/// 블랙홀에 흡수당할 구슬의 정보를 담는 클래스입니다.
/// </summary>
public class BeadData
{
    public Vector2[] pos;
    public Vector2[] velocity;
    public bool[] isActive;

    public BeadData(int count)
    {
        this.pos = new Vector2[count];
        this.velocity = new Vector2[count];
        this.isActive = new bool[count];
    }
}
