using UnityEngine;

/// <summary>
/// 카메라에 부착하는 C# 스크립트입니다.
/// 마우스 휠 입력을 받아서 직교 카메라를 줌 인 / 줌 아웃 합니다.
/// </summary>
public class CameraZoomInOut : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _scrollForce = 2f;
    [SerializeField] private float _minSize = 5f;
    [SerializeField] private float _maxSize = 30f;
    #endregion

    private Camera _camera;

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        // 카메라 검증
        if (_camera == null) {
            De.Print("카메라가 아닙니다.", LogType.Assert);
            enabled = false;
            return;
        }
        // 직교 카메라 검증
        if (!_camera.orthographic) {
            De.Print($"직교 카메라가 아닌 {_camera.name}이 들어왔습니다.", LogType.Assert);
            enabled = false;
        }
    }

    private void Update()
    {
        // 입력
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta == 0f)
            return;
        // 카메라 크기 설정
        float targetSize = _camera.orthographicSize - (scrollDelta * _scrollForce);
        _camera.orthographicSize = Mathf.Clamp(targetSize, _minSize, _maxSize);
    }
    #endregion
}