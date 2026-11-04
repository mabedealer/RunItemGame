using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("生成される弾")]
    public GameObject bullet;

    InputAction shootAction; //Attackアクションボタンの割り振り
    Coroutine shootStart; //シュートされているかどうかを知るための変数


    [Header("弾のスピード/弾速")]
    public float bulletSpeed = 10f; // 弾の飛ぶ速度
    [Header("インターバル時間")]

    public float shootInterval= 0.5f; // 弾の飛ぶインターバル時間

    const int startRemaining = 10; //初期弾数
    private int bulletRemaining = startRemaining; //弾の残数


    //[Header("弾の発射位置")]
    //public Transform muzzle; // 設定しない場合はプレイヤーの位置から発射されます

    //UIなどに残数を伝えるアクセサメソッド
    public int GetRemaining()
    {
        return bulletRemaining;
    }

    //弾数を回復するメソッド
    public void AddBullet(int num)
    {
        bulletRemaining += num;
    }

    void Start()
    {
        //PlayerInputコンポーネントに設定されている現Mapを取得
        InputActionMap actionMap = GetComponent<PlayerInput>().currentActionMap;
        //現マップ上にいる「Attack」というアクションを取得
        shootAction = actionMap.FindAction("Attack");
    }
    void Update()
    {
        //Attackアクションボタンが押された時 かつ 残弾があればShootメソッド
        if (shootAction.WasPerformedThisFrame() && bulletRemaining > 0)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (shootStart == null)
        {
            bulletRemaining--; //残弾を１減らす

            //攻撃開始Bulletプレハブの生成とＺ軸前方へシュート
            GameObject obj = Instantiate(   //生成と同時にそれを変数に代入しておく
                bullet,                     //生成対象オブジェクト
                transform.position,         //生成場所※細かく指定したければGateオブジェなど用意
                Quaternion.identity);       //生成した時のRotation

            Rigidbody rbody = obj.GetComponent<Rigidbody>(); //生成したもののRigidbody
            rbody.AddForce(new Vector3(0, 0, bulletSpeed), ForceMode.Impulse); //前方に押し出す

            //インターバルとなるコルーチンの開始
            shootStart = StartCoroutine(ShootStart());
        }
    }

    //インターバル用コルーチン
    IEnumerator ShootStart()
    {
        //0.5ｆ秒待つ
        yield return new WaitForSeconds(shootInterval);

        //コルーチン変数の解放
        shootStart = null;
    }


    //InputAction shootAction; //Attackアクションボタンの割り振り

    //Coroutine shootStart; //シュートされているかどうかを知るためのコルーチン型の変数

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    //PlayerInputコンポーネントに設定されている現Mapを取得
    //    InputActionMap actionMap = GetComponent<PlayerInput>().currentActionMap;

    //    if (actionMap != null)
    //    {
    //        //現在マップ上にいる「Attack」というアクションを取得
    //        shootAction = actionMap.FindAction("Attack");


    //    }

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //Attackアクションが押された時のShootメソッド


    //    // Attackアクションが押された瞬間（WasPressedThisFrame）にShootメソッドを呼び出す
    //    //if (shootAction != null && shootAction.WasPressedThisFrame())
    //    //if (shootAction.WasPressedThisFrame())
    //    if (shootAction.WasPressedThisFrame() && bulletRemaining > 0)

    //    {
    //        Shoot();
    //    }

    //}
    //void Shoot()
    //{
    //    if (shootStart == null)
    //    {
    //        bulletRemaining--; //残弾を１減らす

    //        //攻撃開始Bulletプレハブの生成とZ軸前方へシュート
    //        //インターバルとなるコルーチンの開始

    //        // 発射位置（muzzleが未設定ならこのオブジェクトの位置）と向きを決める
    //        Vector3 spawnPosition = muzzle != null ? muzzle.position : transform.position + transform.forward;
    //        Quaternion spawnRotation = transform.rotation;

    //        // 攻撃開始：Bulletプレハブの生成
    //        //GameObject spawnedBullet = Instantiate(bullet, spawnPosition, spawnRotation);
    //        GameObject obj = Instantiate(
    //            bullet, //生成対象オブジェクト
    //            transform.position,//生成場所※細かく指定したければGateオブジェクトなどを用意
    //            Quaternion.identity//生成した時のRotation
    //            );


    //        // Z軸前方（またはオブジェクトの正面方向）へ弾を飛ばす（Rigidbodyがある場合）
    //        //Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();

    //        Rigidbody rbody = obj.GetComponent<Rigidbody>();
    //        rbody.AddForce(new Vector3(0, 0, 10.0f), ForceMode.Impulse); //前方Z値に押し出す //(new Vector3(0, 0, 10.0f)はVector値を指名する

    //        //if (rb != null)
    //        //{
    //        //    rb.linearVelocity = transform.forward * bulletSpeed; // ※Unity 6未満の場合は rb.velocity を使ってください
    //        //}

    //        // インターバルとなるコルーチンの開始
    //        shootStart = StartCoroutine(ShootStart());
    //    }

    //}
    ////インターバル用コルーチン
    //IEnumerator ShootStart()
    //{
    //    //0.5f秒待つ
    //    yield return new WaitForSeconds(0.5f);
    //    // 待ち時間が終わったらコルーチン変数をリセットして再発射できるようにする
    //    shootStart = null;


    //}
}
