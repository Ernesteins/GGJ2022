using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float gravity = -5;
    [SerializeField] GameObject loboQuieto;
    [SerializeField] GameObject loboCorre;
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
        if (input.sqrMagnitude > 0)
        {
            transform.forward = input;
            loboQuieto.SetActive(false);
            loboCorre.SetActive(true);
        }
        else
        {
            loboCorre.SetActive(false);
            loboQuieto.SetActive(true);
        }

    }
    private void FixedUpdate()
    {
        if (input.sqrMagnitude > 0)
        {
            controller.Move(input * speed * Time.fixedDeltaTime + Vector3.up * gravity * Time.fixedDeltaTime);
        }
    }
}
