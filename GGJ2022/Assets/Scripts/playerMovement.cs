using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    CharacterController controller;
    Vector3 input;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        input.Normalize();
    }
    private void FixedUpdate() {
        if(input.sqrMagnitude>0){
            controller.Move(input * speed * Time.deltaTime);
        }
    }
}
