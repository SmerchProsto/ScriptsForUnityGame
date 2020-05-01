using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
  bool IsDead, IsRight, IsGround;
  public int speed = 5, jumpForce = 50;
  public LayerMask GroundLayer;
  public Transform GroundCheck;
  Rigidbody2D rb;
  Animator anim;

  // Start is called before the first frame update
  void Start()
  {
    IsRight = true;
    IsDead = false;
    IsGround = true;

    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    CheckHeroIsAlive();
    CheckPressedButton();
    CheckHeroOnGround();
  }

  void CheckHeroIsAlive()
  {
    if (!IsDead)
    {
      Time.timeScale = 1f;
    }
    else
    {
      Time.timeScale = 0f;
    }
  }

  void CheckPressedButton()
  {
    if (Input.GetKey(KeyCode.D))
    {
      if (Input.GetAxis("Horizontal") > 0 && !IsRight)
      {
        Flip();
      }

      anim.SetBool("IsRunning", true);
    }
    else if (Input.GetKey(KeyCode.A))
    {
      if (Input.GetAxis("Horizontal") < 0 && IsRight)
      {
        Flip();
      }

      anim.SetBool("IsRunning", true);
    }
    else
    {
      anim.SetBool("IsRunning", false);
    }

    if (Input.GetKey(KeyCode.W) && IsGround)
    {
      anim.SetTrigger("Jump");
      rb.AddForce(new Vector2(0, jumpForce));
    }

    transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
  }

  void CheckHeroOnGround()
  {
    IsGround = Physics2D.OverlapArea(new Vector2(GroundCheck.position.x - 0.4f, GroundCheck.position.y - 0.2f), new Vector2(GroundCheck.position.x + 0.4f, GroundCheck.position.y + 0.2f), GroundLayer);

    if (IsGround)
    {
      anim.SetBool("IsFalling", false);
    }
    else
    {
      anim.SetBool("IsFalling", true);
    }
  }

  void Flip()
  {
    IsRight = !IsRight;
    Vector3 NewScale = transform.localScale;
    NewScale.x *= -1;
    transform.localScale = NewScale;
  }
}
