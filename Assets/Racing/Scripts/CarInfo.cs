using UnityEngine;

/// <summary>
/// 자동차의 정보를 담는 구조체입니다.
/// </summary>
public struct CarInfo
{
    // 상수
    const float SIZE_X = 0.5f;
    const float SIZE_Y = 0.6f;
    const float SIZE_HALF_X = SIZE_X * 0.5f;
    const float SIZE_HALF_Y = SIZE_Y * 0.5f;
    // 적용 예정
    public float applyX;
    public float applySpeed;
    // 상태
    public float posX;
    public float posY;
    public float speed;
    public float angle;
    public float desiredAngle;
    // 사용 판정
    public bool isDead;

    public void Init(bool isPlayer, float angle)
    {
        this.angle = angle;
        this.desiredAngle = angle;
        this.isDead = true;
    }
}
