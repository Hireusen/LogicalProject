using UnityEngine;

/// <summary>
/// 매 프레임 생성하여 전달하는 읽기 전용 구조체입니다.
/// </summary>
public readonly struct CarFrameInfo
{
    // 입력
    public readonly float horizontal;
    public readonly bool inputAccelerate;
    public readonly bool inputShield;
    public readonly bool inputRush;
    // 카메라
    public readonly Vector2 cameraMinPos;
    public readonly Vector2 cameraMaxPos;
    // 의미있을까?
    public readonly float deltaTime;

    public CarFrameInfo(
        float horizontal, bool inputAccelerate, bool inputShield, bool inputRush,
        Vector2 cameraMinPos, Vector2 cameraMaxPos, float deltaTime)
    {
        this.horizontal = horizontal;
        this.inputAccelerate = inputAccelerate;
        this.inputShield = inputShield;
        this.inputRush = inputRush;
        this.cameraMinPos = cameraMinPos;
        this.cameraMaxPos = cameraMaxPos;
        this.deltaTime = deltaTime;
    }
}