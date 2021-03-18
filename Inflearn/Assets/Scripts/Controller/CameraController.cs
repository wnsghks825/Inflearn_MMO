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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(mode == Define.CameraMode.QuarterView)
        {
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);
        }

    }

    public void SetQuarterView(Vector3 delta)
    {
        mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
