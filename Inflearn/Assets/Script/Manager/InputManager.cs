using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    //Action은 return값 없는 void delegate
    //Func는 return값 존재하는 delegate
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        //InputManager가 대표로 입력을 체크한 후 실제로 입력이 있었다면 그것을 이벤트로 전파하는 형식
        //Listener 패턴

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if(MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if(_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }
}
