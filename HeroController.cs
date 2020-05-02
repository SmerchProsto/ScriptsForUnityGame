using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
  bool isDead, isRight, isGround;
  int attackCost;
  public int speed = 5, jumpForce = 50;
  public LayerMask GroundLayer;
  public Transform GroundCheck;
  Rigidbody2D rb;
  Animator anim;

  // Start is called before the first frame update
  void Start()
  {
    isRight = true;
    isDead = false;
    isGround = true;

    attackCost = 0;

    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    CheckHeroIsAlive();
    CheckHeroOnGround();
    CheckPressedButton();
    
  }

  void CheckHeroIsAlive()
  {
    if (!isDead)
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
      if (Input.GetAxis("Horizontal") > 0 && !isRight)
      {
        Flip();
      }

      anim.SetBool("IsRunning", true);
    }
    else if (Input.GetKey(KeyCode.A))
    {
      if (Input.GetAxis("Horizontal") < 0 && isRight)
      {
        Flip();
      }

      anim.SetBool("IsRunning", true);
    }
    else
    {
      anim.SetBool("IsRunning", false);
    }

    if (Input.GetKey(KeyCode.W) && isGround)
    {
      anim.SetTrigger("Jump");
      rb.AddForce(new Vector2(0, jumpForce));
    }

    if (Input.GetKeyDown(KeyCode.J))
    {
      attackCost++;
      if (attackCost == 3) 
      {
        attackCost = 1;
      }

      anim.SetInteger("NormalAttack", attackCost);
      anim.SetBool("HeroIsNormalAttacking", true);
    }
    else
    {
      anim.SetBool("HeroIsNormalAttacking", false);
    }


    transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
  }

  void CheckHeroOnGround()
  {
    isGround = Physics2D.OverlapArea(new Vector2(GroundCheck.position.x - 0.4f, GroundCheck.position.y - 0.2f), new Vector2(GroundCheck.position.x + 0.4f, GroundCheck.position.y + 0.2f), GroundLayer);

    if (isGround)
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
    isRight = !isRight;
    Vector3 NewScale = transform.localScale;
    NewScale.x *= -1;
    transform.localScale = NewScale;
  }
}
