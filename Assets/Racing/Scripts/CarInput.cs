using System;
using UnityEngine;
/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 유저의 키 입력을 인식해서 이벤트를 뿌립니다.
/// </summary>
public class CarInput : MonoBehaviour
{
    /*
    * [ W ↑ ] 일정하게 가속
    * [ A ↑ ] 오른쪽 이동
    * [ S ↑ ] 일정하게 감속
    * [ D ↑ ] 왼쪽 이동
    * [ Space ] 강하게
    */
    [SerializeField] private KeyCode _hardKey = KeyCode.Space;

    public event Action<bool> OnAccelerate;
    public event Action<bool> OnSlowDown;
    public event Action<bool> OnMoveRight;
    public event Action<bool> OnMoveLeft;

    private void Update()
    {
        // 강하게
        bool isHard = Input.GetKey(_hardKey);
        // 우측 이동
        float h = Input.GetAxis("Horizontal");
        if (h > 0) {
            OnMoveRight?.Invoke(isHard);
        }
        // 왼측 이동
        else if(h < 0) {
            OnMoveLeft?.Invoke(isHard);
        }
        // 가속
        float v = Input.GetAxis("Vertical");
        if (v > 0) {
            OnAccelerate?.Invoke(isHard);
        }
        // 감속
        else if(v < 0) {
            OnSlowDown?.Invoke(isHard);
        }
    }
}
