using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 플레이어 스킬 활성화, 지속시간, 쿨다운을 관리합니다.
/// </summary>
public class CarPlayerSkill : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("보호막")]
    [SerializeField] private float _shieldDuration = 3f;
    [SerializeField] private float _shieldCooldown = 10f;

    [Header("가르기")]
    [SerializeField] private float _rushDuration = 2f;
    [SerializeField] private float _rushCooldown = 8f;
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void UpdateSkill(CarData data, in CarFrameInfo frame)
    {
        float time = Time.time;
        // 보호막 활성화 상태
        if (data.shieldActive) {
            if (time >= data.shieldEndTime) {
                data.shieldActive = false;
            }
        // 보호막 비활성화 상태
        } else if (frame.inputShield && time >= data.shieldNextTime) {
            // 보호막 스킬 사용
            data.shieldActive = true;
            data.shieldEndTime = time + _shieldDuration;
            data.shieldNextTime = time + _shieldDuration + _shieldCooldown;
        }
        // 가르기 활성화 상태
        if (data.rushActive) {
            if (time >= data.rushEndTime) {
                data.rushActive = false;
            }
        // 가르기 비활성화 상태
        } else if (frame.inputRush && time >= data.rushNextTime) {
            // 가르기 스킬 사용
            data.rushActive = true;
            data.rushEndTime = time + _rushDuration;
            data.rushNextTime = time + _rushDuration + _rushCooldown;
        }
    }
    #endregion
}