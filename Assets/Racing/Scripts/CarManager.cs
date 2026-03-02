using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 데이터를 보유하고, 자동차 시스템 스크립트를 호출합니다.
/// </summary>
public class CarManager : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Camera _camera;
    [SerializeField] private RoadScroller _roadScroller;
    [SerializeField] private CarSpawner _carSpawner;
    [SerializeField] private CarRemover _carRemover;
    [SerializeField] private CarPlayerInput _carPlayerInput;
    [SerializeField] private CarPlayerSkill _carPlayerSkill;
    [SerializeField] private CarHorizontal _carHorizontal;
    [SerializeField] private CarVertical _carVertical;
    [SerializeField] private CarComputerAI _carComputerAI;
    [SerializeField] private CarCollision _carCollision;
    [SerializeField] private CarGraphic _carGraphic;
    [SerializeField] private CarDashboard _carDashboard;
    [SerializeField] private CarUI _carUI;

    [Header("도로 설정")]
    [SerializeField] private float _roadWidth = 10f;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private CarData _data;
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Awake()
    {
        // 유효성 검사
        if (De.IsNull(_camera)
            || De.IsNull(_roadScroller)
            || De.IsNull(_carSpawner)
            || De.IsNull(_carRemover)
            || De.IsNull(_carPlayerInput)
            || De.IsNull(_carPlayerSkill)
            || De.IsNull(_carVertical)
            || De.IsNull(_carHorizontal)
            || De.IsNull(_carComputerAI)
            || De.IsNull(_carCollision)
            || De.IsNull(_carGraphic)
            || De.IsNull(_carDashboard)
            || De.IsNull(_carUI)
        ) {
            enabled = false;
            return;
        }
        // 데이터 생성
        _data = new CarData();
        float halfWidth = _roadWidth * 0.5f;
        _data.roadMinX = -halfWidth;
        _data.roadMaxX = halfWidth;
        // 초기화
    }

    private void Update()
    {
        // 프레임 구조체 빌드
        (Vector2 camMin, Vector2 camMax) = URange.GetCameraBounds2D(_camera);
        (float h, bool acceleration, bool shield, bool split) = _carPlayerInput.ReadInput();
        CarFrameInfo frame = new CarFrameInfo(h, acceleration, shield, split, camMin, camMax, Time.deltaTime);

    }
    #endregion
}