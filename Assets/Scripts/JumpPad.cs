using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody _rigidbody = other.GetComponent<Rigidbody>();

            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }
    }


}
