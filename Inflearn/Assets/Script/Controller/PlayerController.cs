using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//float x,y,z를 가지는 구조체. 사용 방법에 따라 두 가지로 분류
//1. 위치 벡터
//2. 방향 벡터

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    Vector3 _destPos;
    bool _moveToDest = false;
    void Start()
    {
        //구독 신청
        Manager.Input.KeyAction -= OnKeyboard;
        Manager.Input.KeyAction += OnKeyboard;
        Manager.Input.MouseAction -= OnMouseClicked;
        Manager.Input.MouseAction += OnMouseClicked;
    }

    private void OnMouseClicked(Define.MouseEvent obj)
    {
        if (obj != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //절대회전값
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        //+- delta
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //1. 바라보는 방향과 최종적으로 바라보기를 원하는 방향간의 각도를 구해서 이동을 시킨다?

        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f)
            {
                _moveToDest = false;
            }
            else
            {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }

    }
        //Update에 작성하면 매우 많은 호출을 하게 된다. 
        //Manager 등에 넣어서 필요로 하는 부분에 대해 이벤트로 넘겨주는 처리를 하면?
    void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            //LookRotation() 특정 방향을 바라보게 만든다.
            //Slerp(), Lerp() 조금 부드럽게 처리되도록.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            //Slerp()으로 부드럽게 처리되도록 해 놓았으므로
            //움직이는 것도 바로 움직이는 것이 아니라 그 중간 지점을 쳐다보고 이동함
            //Translate 사용 시에는 회전도 고려해야 한다. 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += (Vector3.back * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += (Vector3.left * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += (Vector3.right * Time.deltaTime * _speed);
        }
        _moveToDest = false;
    }

}
