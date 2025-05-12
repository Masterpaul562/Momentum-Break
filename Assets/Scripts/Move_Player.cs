using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Player : MonoBehaviour
{

    // Animation
    public Color freezeColor;
    public Color normalColor;
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    bool Grounded;
    public bool armFiringDone = true;

    //Player Input
    private float horizontalInput;
    public KeyCode jumpKey = KeyCode.X;
    public KeyCode attackKey = KeyCode.Z;
    public KeyCode specailAtkKey = KeyCode.C;

    //Movement Variables
    [SerializeField] public float jumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform groundCheck;
    private bool isFacingRight = true;
    public bool doubleJumped;
    public LayerMask whatIsGround;
    Rigidbody2D rb;

    // Normal Punch
    public bool canPunch = true;
    public bool punched;
    public bool isAttacking;
    public bool upperCutDamage;
    public Collider2D UppercutCollider;
    public int knockbackX;
    public int knockbackY;
    public Collider2D punchCollider;
    // Air Attacks
    public bool InAirFall;
    public bool shouldAirDouble;
    public Collider2D UpAirCollider;
    public bool airPunched;
    public bool canAirPunch = true;

    //Special Move Variables
    private bool isInMove = false;
    //Slam
    public float minRotation;
    public float maxRotation;
    private float trackRot;
    [SerializeField] private float rotSpeed;
    [SerializeField] private GameObject arrow;
    Vector2 slamDir;
    private bool slamCoolDown;
    public LayerMask slamTarget;
    public Collider2D slamAOE;
    // Launch Punch
    private bool shouldFire = false;
    private bool punchCoolDown = false;
    public float punchRotSpeed;
    public float minRotationPunch;
    public float maxRotationPunch;
    private float trackRotPunch;
    [SerializeField] private float punchVelocity;
    [SerializeField] private GameObject punchArrow;
    [SerializeField] private GameObject punchProjectile;
    [SerializeField] private Transform spawnLocation;
    
    





    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
      
    }

    void Update()
    {
        animator.SetBool("AirFall", InAirFall);
        
        if (IsGrounded()){
        animator.SetBool("InAirAttack",false) ;
            animator.SetBool("AirFall", false);
            jumpPower = 35;
        //animator.SetBool("IsJumping",false);
       }
        animator.SetBool("Punched", punched);
       
        if (!isInMove  && armFiringDone && !isAttacking)
        {
            Flip();
        }
       
        PlayerInput();
        if (rb.velocity.y > 0&& !isInMove)
        {
            rb.gravityScale = 7;
        } else if (!isInMove) 
       {
           rb.gravityScale = 15;
       }
       
        
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VertSpeed", rb.velocity.y);

    }
    void FixedUpdate()
    {
        onLand();
       
        if (!isInMove && armFiringDone && !isAttacking)
        {            
            Move();
        }
    }
    private void PlayerInput()
    {

        Vector2 facingDirection = new Vector2(transform.localScale.x, 0);
        Vector2 boxCastPos = new Vector2(transform.position.x, transform.position.y-0.3f);
       
            horizontalInput = Input.GetAxisRaw("Horizontal");
        

        // Shooting Punch
        if (Input.GetKey(specailAtkKey)&& punchCoolDown && IsGrounded())
        {
            isInMove = false;
            Input.GetKeyUp(specailAtkKey);
        }
        if (!punchCoolDown)
        {


            if (IsGrounded() && Input.GetKeyDown(specailAtkKey))
            {
                
                shouldFire = !shouldFire;
                rb.velocity = new Vector2(0, 0);
                punchArrow.SetActive(true);
                punchArrow.transform.eulerAngles = new Vector3(0, 0, 90);
                isInMove = true;
                animator.SetBool("IsArmFiring", true);
            }
           
            if (Input.GetKeyUp(specailAtkKey) && IsGrounded()&& shouldFire)
            {
               
                shouldFire= !shouldFire;
                punchCoolDown = !punchCoolDown;
                animator.SetBool("IsArmFiring",false); 
                var newProjectile = Instantiate(punchProjectile, spawnLocation.position, transform.rotation);
               
                if (isFacingRight)
                {
                    Vector2 direction = (spawnLocation.position - punchArrow.transform.position).normalized;
                    newProjectile.GetComponent<Rigidbody2D>().velocity = direction * punchVelocity;
                }
                if (!isFacingRight)
                {
                    Vector2 direction = (spawnLocation.position - punchArrow.transform.position).normalized;
                    newProjectile.GetComponent<Rigidbody2D>().velocity = direction * punchVelocity;
                }
                isInMove = false;
                punchArrow.SetActive(false);
                punchArrow.transform.eulerAngles = new Vector3(0, 0, 90);
                StartCoroutine(startPunchCoolDown());
            }

            if (isInMove && IsGrounded())
            {
                float verInput = Input.GetAxisRaw("Vertical");

                if (isFacingRight)
                {
                    trackRotPunch += verInput * punchRotSpeed * Time.deltaTime;
                    trackRotPunch = Mathf.Clamp(trackRotPunch, minRotationPunch, maxRotationPunch);
                    punchArrow.transform.rotation = Quaternion.Euler(0, 0, trackRotPunch);
                }
                if (!isFacingRight)
                {
                    punchArrow.transform.eulerAngles = new Vector3(0, 0, 260);
                    trackRotPunch -= -verInput * punchRotSpeed * Time.deltaTime;
                    trackRotPunch = Mathf.Clamp(trackRotPunch, minRotationPunch, maxRotationPunch);
                    punchArrow.transform.rotation = Quaternion.Euler(0, 0, -trackRotPunch);

                }

            }
        }
        // Slam
        if (!slamCoolDown) { 
        if (Input.GetKeyDown(specailAtkKey) && !IsGrounded()) {
            rb.velocity = new Vector2(0, 0);
            arrow.SetActive(true);
            spriteRenderer.color = freezeColor;
            animator.SetBool("IsJumping",false);
            animator.SetBool("DoubleJump",false);
                
            if (Input.GetKey(specailAtkKey) && !IsGrounded())
            {
                isInMove = true;
                rb.gravityScale = 0;
                    animator.SetBool("IsInSlam", true);
                
                }
        }
        if (Input.GetKeyUp(specailAtkKey) && !IsGrounded())
        {

            RaycastHit2D endSlamPos = Physics2D.Raycast(arrow.transform.GetChild(1).position, slamDir, Mathf.Infinity, slamTarget);

            if (endSlamPos.collider != null)
            {
                    if (arrow.transform.rotation.z > 0)
                    {
                        transform.position = new Vector3(endSlamPos.point.x-1, endSlamPos.point.y + 1, 0);
                    } else if (arrow.transform.rotation.z < 0)
                    {
                        transform.position = new Vector3(endSlamPos.point.x + 1, endSlamPos.point.y + 1, 0);
                    }
                    else {
                        transform.position = new Vector3(endSlamPos.point.x , endSlamPos.point.y+1, 0);
                    }
                   
                StartCoroutine(HitBoxDer());
                arrow.SetActive(false);
                trackRot = 0;
                isInMove = false;
                arrow.transform.eulerAngles = new Vector3(0, 0, 0);
                spriteRenderer.color = normalColor;
            }
                slamCoolDown = !slamCoolDown;
                animator.SetBool("IsInSlam", false);
                
                StartCoroutine(startSlamCoolDown());
        }
    }
        //Rotation of arrow and direction of slam
        if (isInMove && !IsGrounded())
        {
            trackRot +=  horizontalInput * rotSpeed *Time.deltaTime;
            trackRot = Mathf.Clamp(trackRot, minRotation, maxRotation);
            arrow.transform.rotation = Quaternion.Euler(0, 0, trackRot);
            slamDir = (arrow.transform.GetChild(2).position - arrow.transform.GetChild(1).position).normalized;
        }


        //Normal Attack
        if (Input.GetKeyDown(attackKey) && IsGrounded()&&armFiringDone)
        {
            float vert = Input.GetAxisRaw("Vertical");
            if (canPunch)
            {
                
                rb.velocity = new Vector2(0, 0);
                canPunch = false;
                punched = true;            
                isAttacking = true;     
            
            }
        }
        if (Input.GetKeyDown(attackKey) && !IsGrounded() && !isInMove)
        {
            
            float vert = Input.GetAxisRaw("Vertical");
            if (canAirPunch)
            {
                
                canAirPunch = false;
                airPunched = true;
            }
        }


        //Jumping
        if (IsGrounded() && !Input.GetKey(jumpKey))
        {
            doubleJumped = false;   
        }

if (Input.GetKeyDown(jumpKey) && !isInMove && !isAttacking)
{
    if (IsGrounded() || doubleJumped)
    {
        canAirPunch = true;
                
                animator.SetBool("InAirAttack", false);
                if (InAirFall||shouldAirDouble)
                {
                    shouldAirDouble = false;
                    InAirFall = false;
                    animator.SetTrigger("AirDouble");
                }
                else
        if(doubleJumped)
                {
                    animator.SetBool("DoubleJump", true);
                }
        else { animator.SetBool("IsJumping", true); }
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        doubleJumped = !doubleJumped;
                jumpPower = 35;
            }
}
if (Input.GetKeyUp(jumpKey) && rb.velocity.y > 0f)
{
    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
}
 }


    private void Move()
    {       
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);     
    }

   private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1,0.5f) ,0, whatIsGround);
    }

    private  IEnumerator HitBoxDer()
    {
        slamAOE.enabled = true;        
        yield return new WaitForSeconds(.2f);
        slamAOE.enabled = false;
    }

    
    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f|| !isFacingRight && horizontalInput >0f )
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
           
        }
    }
    private IEnumerator startSlamCoolDown()
    {
        yield return new WaitForSeconds(5f);
        slamCoolDown = !slamCoolDown;
    }
   
    private IEnumerator startPunchCoolDown()
    {
        yield return new WaitForSeconds(3f);
        punchCoolDown = !punchCoolDown;
    }
    private void onLand()
    {
        
        bool wasGround = Grounded;
        Grounded = false;
        if (IsGrounded())
        {
            Grounded = true;
            if (!wasGround)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("DoubleJump", false) ;
            }
        }
    }
    public void ResetPunch() {
        canPunch = true;
    }
   public void UppercutActivation()
    {
        UppercutCollider.enabled = true;
    }
    public void UppercutDeactivation()
    {
        UppercutCollider.enabled = false;
    }
    public void PunchColliderControler()
    {
        punchCollider.enabled = false;
    }
   
}
