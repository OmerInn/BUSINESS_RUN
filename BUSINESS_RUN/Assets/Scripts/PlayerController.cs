using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [Tooltip("Player saga ve sola gidebilecegi max ve min deger")]
    [SerializeField] Vector2 MinMaxPlayerPos;
    [Tooltip("Player saga ve sola gidebilecegi max ve min savrulma degeri")]
    [SerializeField] Vector2 MinMaxPlayerSensivity;
    float distanceFixer = 0;
    private GameObject OffSetObj;
    private Vector3 temp, temp2;
    [Tooltip("Player?n sahip olmas? gereken y?kseklik")]
    public float PlayerPosY;
    Vector3 oldPosition = Vector3.zero;
    public static PlayerController playerController;
    public int Speed = 6;
    public Animator anim;
    public RigidbodyConstraints Rb;   // Start is called before the first frame update
    public Timer timeController;
    public GameManager gameManager;
    public CinemachineVirtualCamera cinemachine;
    public CinemachineTransposer body;
    public Camera cam;
    public GameObject finishTarget, Character;
    public VehiclesController vehiclesController;
    #region Particle
    public ParticleSystem FireWork;
    public ParticleSystem FireWork1;
    Timer timerScript;
    #endregion

    private void Awake()
    {
        playerController = this;   
    }
    void Start()
    {
        OffSetObj = new GameObject(); //
        OffSetObj.name = "OffSetObje";
        OffSetObj.transform.parent = this.transform.parent;
        body = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        FireWork = GetComponent<ParticleSystem>();
        FireWork1 = GetComponent<ParticleSystem>();
        timerScript = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameStarted && !GameManager.isGameEnded&&!GameManager.isGameFailed)
        {
            if (Input.GetMouseButtonDown(0)) //down dokundu?umda bir kere
        {
            OffSetObj.transform.localPosition = new Vector3(CalculateX() * 3, PlayerPosY, 0); //dokundu?um noktay? game sahnesinde alg?lamas?na yarar.
            temp = this.transform.localPosition - OffSetObj.transform.localPosition; //temp=dokundu?um nokta ile offsetim aras?ndaki mesafe.
            distanceFixer = Vector3.Distance(this.transform.position, OffSetObj.transform.localPosition);
            oldPosition = OffSetObj.transform.localPosition; //old positiona ilk dokundu?um noktay? alg?lat?yoruz.
        }
        if (Input.GetMouseButton(0)) //dokundu?um s?rece ?al???r.
        {
            OffSetObj.transform.localPosition = new Vector3(CalculateX() * 3, PlayerPosY, 0);
            temp2 = OffSetObj.transform.localPosition + temp; //dokundu?um nokta ve mesafenin (temp) toplam? temp2 ye e?it olsun.
            //(temp2.y = PlayerPosY; //y sini hep sabit tutmas? i?in e?itliyoruz.
            temp2.z = 0;
            temp2.x = Mathf.Clamp(temp2.x, MinMaxPlayerPos.x, MinMaxPlayerPos.y); //temp2 nin x de?erini min max de?erler aras?na ta??yorum.Y dede ayn?s?.

            this.transform.localPosition = temp2; //pozisyon atamas? yap?l?yor.

            if (distanceFixer - 0.1f > Vector3.Distance(this.transform.position, OffSetObj.transform.position)) //t?klay?p b?rakma i?leminde sa? sol yapmamak i?in vard?r.
            {
                OffSetObj.transform.localPosition = new Vector3(CalculateX() * 3, PlayerPosY, 0);
                temp = this.transform.localPosition - OffSetObj.transform.localPosition;
                distanceFixer = Vector3.Distance(this.transform.position, OffSetObj.transform.localPosition);
            }
        } 
        
        }
    }
    float CalculateX() //Dokundugum nokta ekranda nerede
    {
        Vector3 location = Input.mousePosition;
        return (location.x / (Screen.width / (MinMaxPlayerSensivity.y + Mathf.Abs(MinMaxPlayerSensivity.x)))) - 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.GetComponent<Collider>().enabled = false;
            GameManager.instance.CoinAdd();
            other.gameObject.SetActive(false);

        }
        if (other.GetComponent<TimeController>()!=null)
        {
            if (other.GetComponent<TimeController>().isPlus)
            {
                timeController.time+=5;
                other.gameObject.SetActive(false);
            }
            else
            {
                timeController.time -= 5;
                Debug.Log("minus");
                other.gameObject.SetActive(false);

            } 
        }
        if (other.gameObject.CompareTag("Manhole"))
        {
            PathCreation.Examples.PathFollower.pathFollower.speed = 0;
            Rigidbody Rigid = GetComponent<Rigidbody>();
            Rigid.constraints = RigidbodyConstraints.FreezeRotation;
            GameManager.isGameEnded = true;
            GameManager.instance.OnlevelFailed();
            anim.Play("Idle");

            cinemachine.Follow = null;
            cinemachine.LookAt = null;


        }
        if (other.gameObject.CompareTag("Finish"))
        {
            FireWork.Play();
            FireWork1.Play();           
            body.m_FollowOffset = new Vector3(0, 6f, -15f);
            vehiclesController.Mysing = VehiclesController.Sing.Walking;
            vehiclesController.MinusVehicles();
            GameManager.isGameStarted = false;
            GameManager.isGameWined = true;
            GameManager.isGameEnded = true;
            anim.SetBool("Walk", false);
            anim.SetBool("SlowWalk", true);
            anim.SetBool("SkateBoard", false);
            Character.transform.DOMove(new Vector3(finishTarget.transform.position.x, Character.transform.position.y, finishTarget.transform.position.z), 1.5f)
                .OnComplete(() => { anim.SetBool("Dance", true);anim.SetBool("SlowWalk", false);});
            PathCreation.Examples.PathFollower.pathFollower.speed = 0;
            GameManager.instance.OnlevelCompleted();
        }
    }
    public void PushBack()
    {
        GameManager.instance.isInputEnabled = false;
        StartCoroutine(BackLerp(PathCreation.Examples.PathFollower.pathFollower.speed));
    }
    IEnumerator BackLerp(float oldSpeed)
    {
        PathCreation.Examples.PathFollower.pathFollower.speed = -7.5f;
        PathCreation.Examples.PathFollower.pathFollower.isBackward = true;

        yield return new WaitForSeconds(0.75f);
        PathCreation.Examples.PathFollower.pathFollower.speed = Speed;

        PathCreation.Examples.PathFollower.pathFollower.isBackward = false;
        GameManager.instance.isInputEnabled = true;
        anim.SetBool("SkateBoard", false);
        anim.SetBool("Walk", true);
    }


}
