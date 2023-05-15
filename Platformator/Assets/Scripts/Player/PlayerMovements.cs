using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
  
    Transform playerCoords;
    Rigidbody2D playerRb;

    public float vForce = 450f;
    private float hForce = 2f, plSpeed = 6f;
    private float hDirection = 0f, wallDirIndex = -0.1f;
    private float wallCheckDist = 1f;
    private bool isOnWall;
    [HideInInspector]
    public bool canJump;
    //[HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public AudioClip stepSound;

    [Header("PlayerSetup")]
    [SerializeField] private BoxCollider2D playerCol2D;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimator;
    [Header("Audio")]
    [SerializeField] private AudioSource footAudioSource;
    [SerializeField] private AudioClip jumpSound;

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
        if (!isOnWall) {
            playerAnimator.SetFloat("velocityHorizontal", 0);
            playerAnimator.SetFloat("velocityVertical", playerRb.velocity.y);
        } else {
            playerAnimator.SetFloat("velocityVertical", 0);
        }
        hDirection = Input.GetAxisRaw("Horizontal") * plSpeed;
        if (hDirection != 0) {

            if (hDirection < 0) {
                transform.rotation = Quaternion.Euler(0,0,0);
            } else if (hDirection > 0){
                transform.rotation = Quaternion.Euler(0,180,0);
            }
        }
        if (isGrounded && playerRb.velocity.y == 0) {
            playerAnimator.SetFloat("velocityHorizontal", Mathf.Abs(hDirection));
        } else {
            playerAnimator.SetFloat("velocityHorizontal", 0); 
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
        footAudioSource.clip = jumpSound;
        footAudioSource.Play();
        playerRb.AddForce(Vector2.up * vForce);
    }

    private void StrafeToTheRight(){
        playerRb.AddForce(playerCoords.right * hForce);
    }

    private void StrafeToTheLeft(){
        playerRb.AddForce(playerCoords.right * -1 * hForce);
    }

    private void PlayStepSound() {
        if (isGrounded) {
            footAudioSource.clip = stepSound;
            footAudioSource.Play();
        }
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
