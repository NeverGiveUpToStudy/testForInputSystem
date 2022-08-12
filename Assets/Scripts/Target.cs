using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour {
    private float minSpeed = 14;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;

    private Rigidbody targetRb;
    private GameManager gameManager;
    private Camera cam;
    private InputAction doingPositionAction;

    public int pointValue;
    public ParticleSystem explosionParticle;

    private void Start() {

        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        PlayerInput inputActions = GetComponent<PlayerInput>();
        doingPositionAction = inputActions.actions.FindAction("Position");
    }

    private Vector3 RandomForce() {
        return Vector3.up * UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque() {
        return UnityEngine.Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos() {
        return new Vector3(UnityEngine.Random.Range(-xRange, xRange), ySpawnPos);
    }

    public void OnClick(InputAction.CallbackContext context) {
        //#if UNITY_STANDALONE || UNITY_EDITOR
        //        Vector3 coor = Mouse.current.position.ReadValue();
        //#elif UNITY_ANDROID || UNITY_IOS
        //        Vector3 coor = Touchscreen.current.position.ReadValue();
        //#endif

        //        if (Physics.Raycast(cam.ScreenPointToRay(coor), out hit)) {
        //            if (hit.collider.gameObject == gameObject)
        //                if (gameManager.isGameActive) {
        //                    Destroy(gameObject);
        //                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        //                    gameManager.UpdateScore(pointValue);
        //                }
        //        }

        //        if (context.phase == InputActionPhase.Started) {
        //            Debug.Log("点击Started");
        //        } else if (context.phase == InputActionPhase.Waiting) {
        //            Debug.Log("点击Waiting");
        //        } else if (context.phase == InputActionPhase.Performed) {
        //            Debug.Log("点击Performed");
        //        } else if (context.phase == InputActionPhase.Canceled) {
        //            Debug.Log("点击Canceled");
        //        } else if (context.phase == InputActionPhase.Disabled) {
        //            Debug.Log("点击Disabled");
        //        }

        if (context.phase == InputActionPhase.Performed) {
            RaycastHit hit;
            Vector3 coor = doingPositionAction.ReadValue<Vector2>();
            if (Physics.Raycast(cam.ScreenPointToRay(coor), out hit)) {
                if (hit.collider.gameObject == gameObject)
                    if (gameManager.isGameActive) {
                        Destroy(gameObject);
                        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                        gameManager.UpdateScore(pointValue);
                    }
            }
        } else
            Debug.Log("别的动作暂不处理");
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))//如果加分的箱子没被点掉,触发了这个就会游戏结束
            gameManager.GameOver();
    }

    private void OnBecameInvisible() {
        Debug.Log(gameObject.name + "即将被销毁" + DateTime.Now);
    }
}
