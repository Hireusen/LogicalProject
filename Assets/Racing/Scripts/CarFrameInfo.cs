using UnityEngine;

/// <summary>
/// 매 프레임 생성하여 전달하는 읽기 전용 구조체입니다.
/// </summary>
public readonly struct CarFrameInfo
{
    // 입력
    public readonly float horizontal; // 플레이어 좌우 이동
    public readonly bool enableAccelerate; // 플레이어 가속
    public readonly bool enableShield; // 플레이어 실드
    public readonly bool enableRush; // 플레이어 가르기
    // 카메라
    public readonly Vector2 cameraMinPos;
    public readonly Vector2 cameraMaxPos;
    // 의미있을까?
    public readonly float deltaTime;

    public CarFrameInfo(
        float horizontal, bool accelerate, bool enableShield, bool enableRush,
        Vector2 cameraMinPos, Vector2 cameraMaxPos, float deltaTime)
    {
        this.horizontal = horizontal;
        this.enableAccelerate = accelerate;
        this.enableShield = enableShield;
        this.enableRush = enableRush;
        this.cameraMinPos = cameraMinPos;
        this.cameraMaxPos = cameraMaxPos;
        this.deltaTime = deltaTime;
    }
}