using UnityEngine;

/// <summary>
/// 블랙홀에 흡수당할 구슬의 정보를 담는 클래스입니다.
/// </summary>
public class BeadData
{
    public Vector2[] pos;
    public Vector2[] velocity;
    public int activeCount;
    public int capacity;
    public int absorptionCount;
    public int generatedCount;

    public BeadData(int count)
    {
        this.pos = new Vector2[count];
        this.velocity = new Vector2[count];
        this.activeCount = 0;
        this.capacity = count;
        this.absorptionCount = 0;
        this.generatedCount = 0;
    }
}
