using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeMove : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] private GameObject animatorBody;//Animatorコンポーネントを持っているjオブジェクトの取得
    Animator animator; //Animatorを参照できる変数                                            //Animator animator;


    //Vector3 moveDirection = new Vector3(0,0,0);
    Vector3 moveDirection = Vector3.zero;　//Moveメソッドに与える力
    public float gravity;　//重力値
    public float speedZ; //前に進む力
    public float speedJump; //ジャンプ力


    private InputAction moveAction;
    private InputAction jumpAction;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>(); //Playerについているコンポーネントの取得
        animator = animatorBody.GetComponent<Animator>(); //汎用性を高める
        //animator = GetComponent<Animator>();
        //PlayerInputコンポーネントに設定されている現Mapは何かを取得＝（Playerマップ）
        InputActionMap actionMap = GetComponent<PlayerInput>().currentActionMap;
        moveAction = actionMap.FindAction("Move"); //現Map上にいる"Move"というアクションを取得
        jumpAction = actionMap.FindAction("Jump");//現Map上にいる"Jump"というアクションを取得
    }

    // Update is called once per frame
    void Update()
    {
        //CharacterControllerコンポーネントが接地判断を持っている
        if (controller.isGrounded)
        {
            //上下キーによる前か後ろかの値の設定
            //if (Input.GetAxis("Vertical") > 0.0f) 前だけ動いて、後ろには動かないゼロ
            //if (Input.GetAxis("Vertical") != 0.0f) //上下カーソルキーで前後ろに動く
            if (moveAction.ReadValue<Vector2>().y != 0.0f)

            {
                //moveDirection.z = Input.GetAxis("Vertical") * speedZ;
                moveDirection.z = moveAction.ReadValue<Vector2> ().y * speedZ;

            }
            else
            {
                moveDirection.z = 0;

            }
            //横回転Rotateメソッドで直接回転させる
            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);
            //transform.Rotate(0, Input.GetAxis("Horizontal") * 300* Time.deltaTime, 0);


            //Jumpボタン押されている間
            //if (Input.GetButton("Jump"))
            if(jumpAction.IsPressed())  //if(jumpAction.WasPressedThisFrame()) 
            {
                moveDirection.y = speedJump;　//y方向をセッティング
                //animator.SetTrigger("jump");　//ジャンプアニメへの切り替え
            }
        }
        //常に重力はかかっている
        moveDirection.y -= gravity;
        //moveDirection.y -= gravity * Time.deltaTime;
        //セッティングしたmoveDirectionのｎ値を使ってMoveメソッドの発動
        //※実際にPlayerが動く Time.deltaTimeでPC性能差によらないよう動作安定
        //回転を含む前進だと、グローバルな座標指定からズレるので、グローバルな座標になるように変換してからMoveに与える

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);

        //地面にいるのであればY成分の力を毎回0にリセット
        if (controller.isGrounded) moveDirection.y = 0;
        //アニメの制御
        //animator.SetBool("run", moveDirection.z != 0.0f);
        //animator.SetBool("run", moveDirection.z > 0.0f);

    }

}
