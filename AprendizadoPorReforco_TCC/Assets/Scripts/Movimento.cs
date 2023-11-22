using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;
    private Vector3 inputs;
    private float velocidadeAndar = 2f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(inputs * Time.deltaTime * velocidadeAndar);
        characterController.Move(Vector3.down * Time.deltaTime);

        if (inputs != Vector3.zero)
        {
            animator.SetBool("andandoFrente", true);
            transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10);
        }
        else
        {
            animator.SetBool("andandoFrente", false);
        }
    
    }
}
