using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rigidbody;
    private float h;
    private float v;

    private float moveX;
    private float moveZ;
    private float speedH = 50f;
    private float speedZ = 80f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("JUMP00", -1, 0);
        }

        // -1 ~ +1 까지 H, V 정보를 받아옴.
        h = Input.GetAxis("Horizontal"); // 좌, 우
        v = Input.GetAxis("Vertical"); // 위, 아래

        // animator에 집어넣어 준다.
        animator.SetFloat("h", h);
        animator.SetFloat("v", v);

        // 캐릭터의 이동 제어
        moveX = h * speedH * Time.deltaTime; // 사용자가 입력 한 좌,우에 속도를 곱함.
        moveZ = v * speedZ * Time.deltaTime; // 사용자가 입력 한 상,하에 속도를 곱함.

        if (moveZ <= 0)
        { // 캐릭터가 뒤로 가고 있을 때
            moveX = 0;
        }

        rigidbody.velocity = new Vector3(moveX, 0, moveZ); // x, y, z

    }

    private void OnCollisionEnter(Collision collision) // 충돌 당시 한번만 실행
    {
        if (collision.collider.tag == "Cube")
        {
            Debug.Log("충돌 시작");
            animator.Play("DAMAGED01", -1, 0);
            this.transform.Translate(Vector3.back * speedZ * Time.deltaTime);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Cube")
        {
            Debug.Log("충돌 유지");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Cube")
        {
            Debug.Log("충돌 해제");
        }
    }

}