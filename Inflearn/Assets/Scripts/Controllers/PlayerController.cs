using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 1f;

	Vector3 _destPos;

	//State 패턴
	public enum PlayerState
    {
		//상태를 만들고
		Die,
		Moving,
		Idle
    }
	//기본 상태
	PlayerState _state = PlayerState.Idle;

    void Start()
    {
		//옵저버 패턴
		Managers.Input.MouseAction -= OnMouseClicked;
		Managers.Input.MouseAction += OnMouseClicked;

	}

    void Update()
    {
        switch (_state)
        {
			case PlayerState.Die:
				UpdateDie();
				break;
			case PlayerState.Moving:
				UpdateMoving();
				break;
			case PlayerState.Idle:
				UpdateIdle();
				break;
        }
    }

	//상태에서 상태로 넘어갈 수 있는 방법
	//현재 내 상태에서 적용할 수 있는 코드를 분리해서
    private void UpdateIdle()
    {
		//애니메이션
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("Speed", 0);
	}
    private void UpdateMoving()
    {
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.0001f)
		{
			//목적지에 도달하면 멈춰라
			_state = PlayerState.Idle;
		}
		else
		{
			float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}

		//애니메이션
		Animator anim = GetComponent<Animator>();
		//현재 게임 상태에 대한 정보를 넘겨준다.
		anim.SetFloat("Speed", _speed);
		//스킬 같은 경우는 따로 블렌딩하는 경우가 많다.
    }

    private void UpdateDie()
    {
        throw new NotImplementedException();
    }


	void OnMouseClicked(Define.MouseEvent evt)
	{
		if (_state == PlayerState.Die) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
		{
			_destPos = hit.point;
			_state = PlayerState.Moving;
		}
	}
	
}
