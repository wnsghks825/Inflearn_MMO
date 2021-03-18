using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print($"Collision @ {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"Trigger @ {other.gameObject.name}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
                print($"Raycast Camera @{hit.collider.gameObject.tag} ");
        }

    }
}
