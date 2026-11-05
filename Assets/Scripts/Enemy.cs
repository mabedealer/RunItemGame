using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyLife = 3; //敵の体力
    public GameObject enemyBody; //見た目のオブジェクトの情報

    //無敵時間の秒数
    [Header("無敵時間の設定")]
    //public float invincibilityTime = 1.0f; // 無敵時間の秒数（例: 1秒）
    public float freezeTime = 1.0f; // 無敵時間の秒数（例: 1秒）

    //ダメージ中かどうか
    //private bool isInvincible = false; // ダメージ中（無敵中）かどうかを示すフラグ
    //ダメージ中のコルーチンを参照する変数
    private Coroutine damageStart;

    public GameObject effectObject;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        //もしもダメージコルーチンがnullでなければ（ダメージ中）
        if (damageStart != null)
        {
            //点滅処理
            //valが一定周期で正か負か
            //float val = //三角関数と時間
            //float val = Mathf.Sin(Time.time * 30.0f);
            float val = Mathf.Sin(Time.time * 50);//三角関数と時間

            //if (enemyBody != null)
            if (val > 0)
            {
                //見た目のMeshRendererをON
                enemyBody.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                //見た目のMeshRendererをOFF
                enemyBody.GetComponent<MeshRenderer>().enabled = false;
            }
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //ダメージコルーチンが発動していなければ（ダメージ中でなければ）
        if (damageStart == null)

        {
            //ぶつかった相手がBulletなら
            if (other.gameObject.tag == "Bullet")
            {
                enemyLife--; //体力が１つ減る
                if (enemyLife <= 0)
                {
                    Destroy(gameObject);//消滅
                }
                else //体力が残っていれば
                {
                    damageStart = StartCoroutine(DamageStart()); //コルーチンの発動
                }
            }
            //ぶつかった時
            else if (other.gameObject.tag == "Slash")
            {
                Destroy(gameObject);

            }

        }
    }
    //時間差でダメージ状態を解くコルーチン
    IEnumerator DamageStart()
    {
        //freezeTime待ってから、ダメージコルーチン変数を解放
        yield return new WaitForSeconds(freezeTime);
        damageStart = null;
        //見た目のMeshRendererをON
        enemyBody.GetComponent<MeshRenderer>().enabled = true;


    }
    void CreateEffect()
    {
        Instantiate(
            effectObject,
            transform.position,
            Quaternion.identity);

    }
}

