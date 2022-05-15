using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimation;
  
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier;
    [SerializeField] bool isOnGround = true;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem dirtParticle;

    [SerializeField] AudioClip jumpSound; 
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip gameOverSound;

    private AudioSource playerAudio;

    public bool gameOver = false;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 500);
        playerAnimation = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
            {
                playerAnimation.SetTrigger("Jump_trig");
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
     
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();

            explosionParticle.Play();
            dirtParticle.Stop();
            
            gameOver = true;
            
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(gameOverSound, 1.0f);
        }
    }
}
