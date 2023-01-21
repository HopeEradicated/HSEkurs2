using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
  
    Transform playerCoords;
    Rigidbody2D playerRb;

    private float vForce = 450f/*Мб поменять название*/, hForce = 2f, plSpeed = 6f;
    private float hDirection = 0f, wallDirIndex = -0.1f;
    private float wallCheckDist = 2f;
    private bool canJump, isOnWall;

    private BoxCollider2D playerCol2D;

    private void Start() {
        playerCoords = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        playerCol2D = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (canJump){
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && !Input.GetKey(KeyCode.S)){
                Jump();
                canJump = false;
            } else if (isOnWall){
                //Могут возникать проблемы, если на карте будут соприкасаться платформы и стены. Потенциально - поменять
                if (playerRb.velocity.y < plSpeed){
                    playerRb.velocity = new Vector2(playerRb.velocity.x, wallDirIndex*plSpeed);
                }
            }
        }
        hDirection = Input.GetAxisRaw("Horizontal") * plSpeed;
        
       
        /*Оставить, как альтернативную физику движения, для другого персонажа или класса, например
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            StrafeToTheRight();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            StrafeToTheLeft();
        }
        */

        if (Input.GetKey(KeyCode.F)){
            playerCoords.localPosition = Vector3.Lerp(playerCoords.localPosition, 
            new Vector3(0, 1f,0), 5f);
        }
    }

    private void FixedUpdate() {
        //Переписать, чтобы игрок мог спрыгивать со стен
        Physics2D.queriesStartInColliders = false;
        var raycastRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallCheckDist);
        var raycastLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallCheckDist);
        //Тут два ифа бесполезных
        if (raycastRight.collider == null && raycastLeft.collider == null) {
            playerRb.velocity = new Vector2(hDirection, playerRb.velocity.y);
        }else if (raycastRight.collider != null && raycastRight.collider.gameObject.tag == "Wall" && hDirection == -1) {
            playerRb.velocity = new Vector2(hDirection, playerRb.velocity.y);
        } else if (raycastLeft.collider != null && raycastLeft.collider.gameObject.tag == "Wall" && hDirection == 1) {
            playerRb.velocity = new Vector2(hDirection, playerRb.velocity.y);
        } else if ((raycastRight.collider != null && raycastRight.collider.gameObject.tag != "Wall") || (raycastLeft.collider != null && raycastLeft.collider.gameObject.tag != "Wall")) {
            playerRb.velocity = new Vector2(hDirection, playerRb.velocity.y);
        }

    }

    private void Jump(){
        playerRb.AddForce(playerCoords.up * vForce);
    }

    private void StrafeToTheRight(){
        playerRb.AddForce(playerCoords.right * hForce);
    }

    private void StrafeToTheLeft(){
        playerRb.AddForce(playerCoords.right * -1 * hForce);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        canJump = true;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Wall"){
            isOnWall = true;
        } else if (Input.GetKey(KeyCode.S)) {
            //Реализовываем механику спрыгивания с платформы
            playerCol2D.isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Wall"){
            isOnWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        canJump = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        playerCol2D.isTrigger = false;
    }
}
