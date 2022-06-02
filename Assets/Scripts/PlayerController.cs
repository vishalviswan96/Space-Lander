using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// This Script controls the player. It controls rotation and movement of the player also does ground Check work.
    /// 
    /// </summary>

    [Header("Movement Controls")]

    [SerializeField] private float force_Amount = 1f;
    [SerializeField] private float rotate_Amount = 1f;
    [SerializeField] private float transition_Time = 1f;

    [Header("Particle Effects")]

    [SerializeField] private ParticleSystem rocket_Particle, win_Particle, lose_Particle;

    private float xInput;
    private Collision2D col;
    private enum State {LOOSE, ALIVE }
    private State player_state = State.ALIVE;

    private Rigidbody2D my_Body;
    [SerializeField] private SpriteRenderer sr;

    [Header("Ground Check")]

    [SerializeField] float check_Radius = 0.3f;
    public LayerMask ground_Layer;
    public Transform ground_Check;
    bool is_Ground;

    [SerializeField] public ButtonScript button_Script;

    private void Awake()
    {
        my_Body = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        if (player_state == State.ALIVE)
        {
            HandleRotation();
        }
    }

    //FixedUpdate is called at fixed time interval thus it handles the physics based ridigidbody movements
    private void FixedUpdate()
    {
        if (player_state == State.ALIVE)
        {
            HandleMovement();
        }

        //ImpulseJump();
        

    }

    public void HorizontalInput(float value)
    {
        xInput = value;
    }


    //This Method rotates the player along its axis
    void HandleRotation()
    {
        my_Body.freezeRotation = true;

        float rotation_This_Frame = rotate_Amount * Time.deltaTime;

        if (xInput < 0)
        {
            transform.Rotate(Vector3.forward * rotation_This_Frame);
        }
        else if (xInput > 0)
        {
            transform.Rotate(-Vector3.forward * rotation_This_Frame);
        }

        my_Body.freezeRotation = false;
    }

    void HandleMovement()
    {
        if(Mathf.Abs(xInput) > 0)
        {
            my_Body.AddRelativeForce(Vector3.up * force_Amount);
            rocket_Particle.Play();
        }
    }


    private void OnCollisionEnter2D(Collision2D target)
    {
        if (player_state != State.ALIVE)
            return;
        col = target;
        is_Ground = Physics2D.OverlapCircle(ground_Check.position, check_Radius, ground_Layer);

        if (is_Ground)
        {
            Debug.Log(target.relativeVelocity.magnitude);
            // Hard hit on platform leading to explosion
            if (target.relativeVelocity.magnitude > 8)
            {
                Debug.Log(target.relativeVelocity.magnitude);
                sr.enabled = false;
                button_Script.button_Active = false;
                Invoke("CheckStatus", transition_Time);
                lose_Particle.Play();
                AudioManager.instance.PlayAudio(1);
            }
            else
            {
                // Secure landing leadind to continue of the game
                Debug.Log("RESET");
                CheckStatus();
                win_Particle.Play();
                AudioManager.instance.PlayAudio(0);
            }
        }
        
    }

    void CheckStatus()
    {
        sr.enabled = true;
        transform.position = new Vector3(col.transform.position.x, col.transform.position.y + 1.15f);
        transform.rotation = Quaternion.identity;
        my_Body.velocity = Vector2.zero;
        button_Script.ResetButton();
    }
    

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlayerDied()
    {
        sr.enabled = false;
        player_state = State.LOOSE;
        Invoke("RestartLevel", transition_Time);
    }

}
