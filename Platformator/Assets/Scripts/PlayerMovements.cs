using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
  
    Transform playerCoords;
    Rigidbody2D playerRb;

    private float vForce = 500f/*Мб поменять название*/, hForce = 2f;
    private bool canMove;

    private void Start() {
        playerCoords = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (canMove){
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)){
                Jump();
                canMove = false;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                StrafeToTheRight();
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                StrafeToTheLeft();
            }
        }
        if (Input.GetKey(KeyCode.F)){
            playerCoords.localPosition = Vector3.Lerp(playerCoords.localPosition, 
            new Vector3(0, 1f,0), 5f);
        }
    }

    private void Jump(){
        playerRb.AddForce(playerCoords.up * vForce);
        //playerCoords.localPosition = Vector2.MoveTowards( playerCoords.localPosition, new Vector2(playerCoords.localPosition.x, playerCoords.localPosition.y + 3), vSpeed * Time.deltaTime);  
    }

    private void StrafeToTheRight(){
        playerRb.AddForce(playerCoords.right * hForce);
        //playerCoords.localPosition = Vector2.MoveTowards( playerCoords.localPosition, new Vector2(newHorCoords, playerCoords.localPosition.y), hSpeed * Time.deltaTime);
    }

    private void StrafeToTheLeft(){
        playerRb.AddForce(playerCoords.right * -1 * hForce);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground" && !canMove){
            canMove = true;
        }
    }
}

//Почему игрок уезжает вправо? Потому что в определённый момент сила даётся куда-то в пизду и он не понимает, что нужно прыгать. UPD: он просто крутиться, пофиксить джамп
//Идея: сделать зацеп, когда игрок касается вертикальной части платформы
