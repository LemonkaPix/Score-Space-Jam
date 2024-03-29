using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public bool isDashing = false;
    public float dashSpeed;
    public int dashDamage;
    Vector2 movement;
    [SerializeField] PlayerController pc;
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (isDashing)
            rb.MovePosition(rb.position + new Vector2(transform.up.x, transform.up.y) * dashSpeed * Time.fixedDeltaTime);
        else
                {
            rb.MovePosition(rb.position + movement * (pc.plrSpeed + (pc.SpeedLevel * pc.SpeedValue)) * Time.fixedDeltaTime);
        }
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
