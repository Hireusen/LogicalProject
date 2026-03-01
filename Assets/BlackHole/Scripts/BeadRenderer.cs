using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 활성화된 구슬을 카메라 화면에 그립니다.
/// </summary>
public class BeadRenderer : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    [Header("사용자 정의 설정")]
    [SerializeField] private float _circleSize = 0.5f;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    const int MATRIX_SIZE = 1023;
    Matrix4x4[] _matrices = new Matrix4x4[MATRIX_SIZE];
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    private static void SetMatrix2D(ref Matrix4x4 matrix, Vector2 pos, float size)
    {
        matrix.m03 = pos.x;
        matrix.m13 = pos.y;
        matrix.m00 = size; // Scale X
        matrix.m11 = size; // Scale Y
    }

    public void RenderBeads(Vector2[] beadsPos, float[] beadsSize, int activeCount)
    {
        // 변수 준비
        int batchCount = 0;
        float defaultSize = _circleSize;
        // 모든 구슬 순회
        for (int i = 0; i < activeCount; ++i) {
            // 좌표 설정
            SetMatrix2D(ref _matrices[batchCount], beadsPos[i], defaultSize * beadsSize[i]);
            // 구슬 1023개 그리기
            batchCount++;
            if (batchCount >= MATRIX_SIZE) {
                Graphics.DrawMeshInstanced(_mesh, 0, _material, _matrices);
                batchCount = 0;
            }
        }
        // 남은 구슬 그리기
        if (batchCount > 0) {
            Graphics.DrawMeshInstanced(_mesh, 0, _material, _matrices, batchCount);
        }
    }

    private void InitMatrices()
    {
        // Matrix4x4 버퍼 초기화해두기
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix.m00 = _circleSize; // Scale X
        matrix.m11 = _circleSize; // Scale Y
        for (int i = 0; i < MATRIX_SIZE; ++i) {
            _matrices[i] = matrix;
        }
    }

    private void Awake()
    {
        InitMatrices();
        // 유효성 검사
        if(De.IsNull(_mesh) || De.IsNull(_material)) {
            enabled = false;
        }
    }

    private void OnValidate()
    {
        InitMatrices();
    }
    #endregion
}
