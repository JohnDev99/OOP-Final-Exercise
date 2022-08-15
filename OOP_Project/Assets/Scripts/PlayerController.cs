using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    private Rigidbody rb;
    [SerializeField] float jumpForce = 10f;
    private bool inWater;

    [SerializeField] Transform mounthPos;
    [SerializeField] float radius = 2f;
    [SerializeField] LayerMask comsumeLayer;
    // Start is called before the first frame update
    [SerializeField] ParticleSystem bloodParticles;
    [SerializeField] ParticleSystem splashParticles;
    public ParticleSystem deathVfx;

    public AudioClip[] soundEffects;
    public AudioSource playerAudioSource;
    public float soundsVolume = 1f;

    //Proteger dados da variabel
    [SerializeField] int myPoints;

    //Vida do Player
    private float life;
    public float Life {
        get { return life; }
        set {
            if (life <= 0)
            {
                life = 0;
            }
            else if (life > 100f)
            {
                life = 100f;
            }
            else
            {
                life = value;
            }

        }
    }

    MainManager mainManager;


    /// <summary>
    /// Propriedade que retorna os meus pontos do jogo
    /// </summary>
    public int PlayerPoints
    {
        get { return myPoints; }
        set
        {
            if (myPoints <= 0)
            {
                myPoints = 0;
            }
            else
            {
                myPoints = value;
            }
        }
    }

    private void Awake()
    {
        mainManager = FindObjectOfType<MainManager>();
    }

    //Criar mecanica de rotaçao do corpo que vai até -45º a 45º, e segue
    //o movimento do player


    void Start()
    {

        rb = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
        inWater = true;
        myPoints = 0;
        life = 100f;


    }

    // Update is called once per frame
    void Update()
    {
        if (mainManager.IsGameRunning == true)
        {
            CollideWithFish();
            DecreaseLife();
            PlayerBoundries();
        }


    }

    private void CollideWithFish()
    {
        Collider[] fish = Physics.OverlapSphere(mounthPos.position, radius, comsumeLayer);
        foreach (Collider myFish in fish)
        {
            EatFish(myFish);

        }
    }

    private void EatFish(Collider myFish)
    {
        if (myFish.gameObject.tag == "fish")
        {
            myPoints += myFish.gameObject.GetComponent<Fish>().pointsToGive;
            FishEat(myFish);
        }
        if (myFish.gameObject.tag == "salmon")
        {
            FishEat(myFish);
            life += myFish.GetComponent<Salmon>().lifeToPlayer;
        }

        //Play sound
        playerAudioSource.PlayOneShot(soundEffects[2], soundsVolume);
    }

    private void FishEat(Collider myFish)
    {
        bloodParticles.Play();
        Destroy(myFish.gameObject);
    }
    private void OnDrawGizmos()
    {
        //Se a minha variavel nao tiver nenhum valor armazenado, nao desenhar o Gizmos
        if (mounthPos == null)
            return;
        Gizmos.DrawSphere(mounthPos.position, radius);
    }

    private void PlayerBoundries()
    {
        if (transform.position.y < -minY)
        {
            transform.position = new Vector3(transform.position.x, -minY, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (mainManager.IsGameRunning)
        {
            Move();
        }


    }
    private void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if(inWater == true)
        {
            transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);
        }

    }

    //Quando player sair fora do trigger , adicionar uma força de impulso
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ocean"))
        {
            Jump();
            splashParticles.Play();
        }

        

    }

    private void Jump()
    {
        //Corpo fisico
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.useGravity = true;
        inWater = false;
        //Play waterSound
        playerAudioSource.PlayOneShot(soundEffects[1], soundsVolume);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ocean"))
        {
            rb.useGravity = false;
            inWater = true;
            rb.velocity = Vector3.zero;
            splashParticles.Stop();
            playerAudioSource.PlayOneShot(soundEffects[1], soundsVolume);
        }

    }

    void DecreaseLife()
    {
        life -= Time.deltaTime;
    }
    
}
