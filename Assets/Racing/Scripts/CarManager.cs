using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// Car 관련 스크립트를 호출합니다.
/// </summary>
public class CarManager : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private CarInput _carInput;
    [SerializeField] private CarMover _carMover;
    [SerializeField] private CarGraphic _carGraphic;
    [SerializeField] private GameObject _playerCar;
    [SerializeField] private GameObject _computerCar;

    [Header("게임 설정")]
    [SerializeField] private bool _playing = true;
    [SerializeField] private int _carCount = 10;
    [SerializeField] private float _spawnSeconds = 1f;
    [SerializeField] private Vector2 _chestPos = new Vector2(10f, 0f);
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private const float DEFAULT_ANGLE = 270f; // Z angle
    private CarInfo[] _carInfos;
    private Transform[] _carTR;
    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────
    private void CreateCars(out CarInfo[] carInfos, out Transform[] carTR)
    {
        carInfos = new CarInfo[_carCount];
        carTR = new Transform[_carCount];
        Quaternion baseRotation = Quaternion.Euler(0f, 0f, DEFAULT_ANGLE);
        // 플레이어 자동차 생성
        {
            GameObject go = Instantiate(_playerCar, _chestPos, baseRotation);
            _carTR[0] = go.transform;
            carInfos[0].Init(true, DEFAULT_ANGLE);
            go.SetActive(false);
        }
        // 컴퓨터 자동차 생성
        for (int i = 1; i < _carCount; ++i) {
            GameObject go = Instantiate(_computerCar, _chestPos, baseRotation);
            _carTR[i] = go.transform;
            carInfos[i].Init(false, DEFAULT_ANGLE);
            go.SetActive(false);
        }
    }

    private void InitGame()
    {
        // 모두 비활성화
        for (int i = 0; i < _carCount; ++i) {
            _carTR[i].gameObject.SetActive(false);
            _carInfos[i].isDead = true;
        }
    }
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    // 유효성 검사
    private void Awake()
    {
        if (_carInput == null) {
            De.Print($"Car Input is NULL", LogType.Assert);
            enabled = false;
        }
        if (_carMover == null) {
            De.Print($"Car Mover is NULL", LogType.Assert);
            enabled = false;
        }
        if (_carGraphic == null) {
            De.Print($"Car Graphic is NULL", LogType.Assert);
            enabled = false;
        }
        if (_playerCar == null) {
            De.Print($"플레이어 자동차 프리펩을 등록해주세요.", LogType.Assert);
            enabled = false;
        }
        if (_computerCar == null) {
            De.Print($"컴퓨터 자동차 프리펩을 등록해주세요.", LogType.Assert);
            enabled = false;
        }
    }

    private void Start()
    {
        CreateCars(out _carInfos, out _carTR);
    }

    private void Update()
    {
        if (!_playing) {
            return;
        }
        _carMover.UpdateCars(_carInfos, _carCount);
    }

    private void LateUpdate()
    {
        if (!_playing) {
            return;
        }
        _carGraphic.UpdateCenterPos(_carInfos[0].posX, _carInfos[0].posY);
        _carGraphic.UpdateCarGraphic(ref _carInfos, _carCount);
        _carGraphic.DrawScreenBounds();
    }

    private void OnValidate()
    {
        if (_carCount < 2) {
            _carCount = 2;
        }
        if (_spawnSeconds < 0f) {
            _spawnSeconds = 0f;
        }
    }
    #endregion
}
