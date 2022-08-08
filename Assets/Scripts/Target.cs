using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour {
    private Rigidbody targetRb;
    private float minSpeed = 14;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    private GameManager gameManager;

    public int pointValue;
    public ParticleSystem explosionParticle;

    private Camera cam;

    // Start is called before the first frame update
    private void Start() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private Vector3 RandomForce() {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque() {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos() {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    public void OnClick(InputAction.CallbackContext context) {
        RaycastHit hit;


#if UNITY_STANDALONE || UNITY_EDITOR
        Vector3 coor = Mouse.current.position.ReadValue();
#elif UNITY_ANDROID || UNITY_IOS
        Vector3 coor = Touchscreen.current.position.ReadValue();
#endif

        if (Physics.Raycast(cam.ScreenPointToRay(coor), out hit)) {
            if (hit.collider.gameObject == gameObject)
                if (gameManager.isGameActive) {
                    Destroy(gameObject);
                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                    gameManager.UpdateScore(pointValue);
                }
        }

        if (context.phase == InputActionPhase.Started) {
            Debug.Log("点击Started");
        } else if (context.phase == InputActionPhase.Waiting) {
            Debug.Log("点击Waiting");
        } else if (context.phase == InputActionPhase.Performed) {
            Debug.Log("点击Performed");
        } else if (context.phase == InputActionPhase.Canceled) {
            Debug.Log("点击Canceled");
        } else if (context.phase == InputActionPhase.Disabled) {
            Debug.Log("点击Disabled");
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))//如果加分的箱子没被点掉,触发了这个就会游戏结束
            gameManager.GameOver();
    }
}
