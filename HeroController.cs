﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
  bool isDead, isRight, isGround, isCollide;
  bool jumpOffEnable;

  int attackCost, playerObject, collideObject;

  public int speed = 5, jumpForce = 50;

  public LayerMask GroundLayer, collideLayer;
  public Transform GroundCheck;

  Rigidbody2D rb;
  Animator anim;


  // Start is called before the first frame update
  void Start()
  {
    isCollide = false;
    isRight = true;
    isDead = false;
    isGround = true;

    jumpOffEnable = false;

    attackCost = 0;

    playerObject = LayerMask.NameToLayer("player");
    collideObject = LayerMask.NameToLayer("collide");

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

    if ((Input.GetKey(KeyCode.W) && isGround) || (Input.GetKey(KeyCode.W) && isCollide))
    {
      anim.SetTrigger("Jump");
      rb.AddForce(new Vector2(0, jumpForce));
    }

    if (Input.GetKey(KeyCode.S))
    {
      StartCoroutine("JumpOff");
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

    if (Input.GetKeyDown(KeyCode.K))
    {
      anim.SetTrigger("HardAttack");
    }


    transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
  }

  void CheckHeroOnGround()
  {
    isGround = Physics2D.OverlapArea(new Vector2(GroundCheck.position.x - 0.1f, GroundCheck.position.y - 0.05f), new Vector2(GroundCheck.position.x + 0.1f, GroundCheck.position.y + 0.05f), GroundLayer);
    isCollide = Physics2D.OverlapArea(new Vector2(GroundCheck.position.x - 0.1f, GroundCheck.position.y - 0.05f), new Vector2(GroundCheck.position.x + 0.1f, GroundCheck.position.y + 0.05f), collideLayer);
    /*  // Debug Func
    if (isGround)
    {
      print("На земле");
    }
    else if (isCollide)
    {
      print("На платформе");
    }
    else
    {
      print("Не на чем");
    }
*/

    if (isGround || isCollide)
    {
      anim.SetBool("IsFalling", false);
    }
    else
    {
      anim.SetBool("IsFalling", true);
    }

    if (rb.velocity.y > 0)
    {
      isCollide = false;
      Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
    }
    else
    {
      Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
    }
    

  }

  IEnumerator JumpOff()
  {
    jumpOffEnable = true;
    Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
    yield return new WaitForSeconds(0.5f);
    Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
    jumpOffEnable = false;
  }

  void Flip()
  {
    isRight = !isRight;
    Vector3 NewScale = transform.localScale;
    NewScale.x *= -1;
    transform.localScale = NewScale;
  }
}
