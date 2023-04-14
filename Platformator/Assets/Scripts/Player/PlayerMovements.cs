using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
  
    Transform playerCoords;
    Rigidbody2D playerRb;

    private float vForce = 450f/*Мб поменять название*/, hForce = 2f, plSpeed = 6f;
    private float hDirection = 0f, wallDirIndex = -0.1f;
    private float wallCheckDist = 1f;
    private bool isOnWall;
    [HideInInspector]
    public bool canJump;

    private BoxCollider2D playerCol2D;
    [SerializeField]
    private SpriteRenderer playerSprite;

    private void Start() {
        playerCoords = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        playerCol2D = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (canJump){
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && !Input.GetKey(KeyCode.S)){
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                Jump();
                canJump = false;
            } else if (isOnWall){
                if (playerRb.velocity.y < plSpeed){
                    playerRb.velocity = new Vector2(playerRb.velocity.x, wallDirIndex*plSpeed);
                }
            }
        }
        hDirection = Input.GetAxisRaw("Horizontal") * plSpeed;
        
        if (hDirection < 0) {
            playerSprite.flipX = true;
        } else if (hDirection > 0){
            playerSprite.flipX = false;
        }
        /*Оставить, как альтернативную физику движения, для другого персонажа или класса, например
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            StrafeToTheRight();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            StrafeToTheLeft();
        }
        */

        if (Input.GetKey(KeyCode.F)){
            playerCoords.localPosition = Vector3.Lerp(playerCoords.localPosition, new Vector3(0, 1f,0), 5f);
        }
    }

    private void FixedUpdate() {
        Physics2D.queriesStartInColliders = false;
        var raycastRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallCheckDist);
        var raycastLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallCheckDist);

        bool isSomeBlockingObjectOnTheLeft = (raycastLeft.collider != null && (raycastLeft.collider.gameObject.tag == "Wall" || raycastLeft.collider.gameObject.tag == "Border"));
        bool isSomeBlockingObjectOnTheRight = (raycastRight.collider != null && (raycastRight.collider.gameObject.tag == "Wall" || raycastRight.collider.gameObject.tag == "Border"));
        if ((isSomeBlockingObjectOnTheRight && hDirection < 0 ) || (isSomeBlockingObjectOnTheLeft && hDirection > 0) || (!isSomeBlockingObjectOnTheLeft && !isSomeBlockingObjectOnTheRight)) {
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

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Wall"){
            canJump = true;
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

    private void OnTriggerExit2D(Collider2D other) {
        playerCol2D.isTrigger = false;
    }
}
