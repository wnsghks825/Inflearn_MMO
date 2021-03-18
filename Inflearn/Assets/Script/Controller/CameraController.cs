using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode mode = Define.CameraMode.QuarterView;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f,6.0f,-5.0f);//플레이어 기준으로 얼마나 떨어져 있나
    [SerializeField]
    GameObject _player = null;


    //플레이어 기준으로 카메라 위치에게 좌표를 쏴 준다. Collision이 있다면 카메라를 이동시키면 된다.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(mode == Define.CameraMode.QuarterView)
        { 
            RaycastHit hit;
            if ( Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                //벽을 만났다.
                //벽과 플레이어의 거리를 구한 후 조금 앞으로 이동시킨다.
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }

        }

    }

    public void SetQuarterView(Vector3 delta)
    {
        mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
