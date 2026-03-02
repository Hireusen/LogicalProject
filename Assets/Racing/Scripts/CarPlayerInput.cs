using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 플레이어의 키 입력을 읽어 반환합니다.
/// </summary>
public class CarPlayerInput : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private KeyCode _accelerateKey = KeyCode.Space;
    [SerializeField] private KeyCode _shieldKey = KeyCode.Q;
    [SerializeField] private KeyCode _rushKey = KeyCode.E;
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public (float horizontal, bool accelerate, bool shield, bool rush) ReadInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        bool accelerate = Input.GetKey(_accelerateKey);
        bool shield = Input.GetKeyDown(_shieldKey);
        bool rush = Input.GetKeyDown(_rushKey);
        return (h, accelerate, shield, rush);
    }
    #endregion
}