using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public int heroSpeed;

  public float cameraMaxPositionX, cameraMaxPositionY, cameraMinPositionX, cameraMinPositionY;
  public static float cameraInitialPosX, cameraFinalPosX, cameraInitialPosY, cameraFinalPosY;

  Camera mainCamera;

  public GameObject hero;

  Renderer rend;


  Vector2 cameraMinCoordsPosition;
  Vector2 cameraMaxCoordsPosition;

  // Start is called before the first frame update
  void Start()
  {
    heroSpeed = 5;

    rend = GetComponent<Renderer>();

    mainCamera = Camera.main;
  }

  // Update is called once per frame
  void Update()
  {
    MoveCamera(); // Передвигаем камеру вместе с игроком
    CheckPosition(); // Камера не выходит за определенную область
    MakeCameraBorders(); // Границы камеры, за которые не выходит
    //print("Позиция камеры: " + transform.position);
    //print("Позиция игрока: " + hero.transform.position);

    
  }

  void FreezeCameraPosition(float cameraPosX, float cameraPosY)
  {
    transform.position = new Vector3(cameraPosX, cameraPosY, transform.position.z);
  }

  void DeFreezeCameraPosition()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
  }

  void MakeCameraBorders()
  {
    cameraMinCoordsPosition = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
    cameraMaxCoordsPosition = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

    cameraInitialPosX = cameraMinCoordsPosition.x;
    cameraFinalPosX = cameraMaxCoordsPosition.x;

    cameraInitialPosY = cameraMinCoordsPosition.y;
    cameraFinalPosY = cameraMaxCoordsPosition.y;
  }


  void CheckPosition()
  {
    if (transform.position.x <= cameraMinPositionX)
    {
      transform.position = new Vector3(cameraMinPositionX, transform.position.y, transform.position.z);
    }
    if (transform.position.y <= cameraMinPositionY)
    {
      transform.position = new Vector3(transform.position.x, cameraMinPositionY, transform.position.z);
    }
    if (transform.position.x >= cameraMaxPositionX)
    {
      transform.position = new Vector3(cameraMaxPositionX, transform.position.y, transform.position.z);
    }
    if (transform.position.y >= cameraMaxPositionY)
    {
      transform.position = new Vector3(transform.position.x, cameraMaxPositionY, transform.position.z);
      transform.position = new Vector3(transform.position.x, cameraMaxPositionY, transform.position.z);
    }
  }

  void MoveCamera()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    if (hero.transform.position.x >= transform.position.x && Input.GetAxis("Horizontal") > 0)
    {
      transform.Translate(Input.GetAxis("Horizontal") * heroSpeed * Time.deltaTime, 0, 0);
    }

  }

}
