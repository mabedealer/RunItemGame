using UnityEngine;


//変数deleteTimeを用意
//生成されたらdeleteTime秒後に消える
//誰かにあたったら消える
public class BulletController : MonoBehaviour
{
    [Header("弾が自動消滅時間")]
    public float deleteTime = 3.0f; //インスペクターで値を変更する

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //生成されてからdeleteTime秒後にこのオブジェクトを破壊する（削除）する
        Destroy(gameObject, deleteTime);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        //何かにぶつかったら消滅
        Destroy(gameObject);
    }

    ////物理的な衝突（コリジョン）で消したい場合
    //void OnCollisionEnter(Collision collision)
    //{
    //    //何かに衝突した瞬間にこのオブジェクトを破壊する
    //    Destroy(gameObject);
    //}

    /* 
   // --- 2Dゲームや、Trigger（すり抜け判定）を使いたい場合はこちらを有効にしてください ---
   void OnTriggerEnter2D(Collider2D collision)
   {
       Destroy(gameObject);
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
       Destroy(gameObject);
   }
   */
}
