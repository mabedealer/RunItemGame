using UnityEngine;

public enum ItemType
{
    Life,
    Bullet
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        //相手がプレイヤーなら
        if (other.gameObject.tag == "Player")
        {
            //タイプによってやることが異なる
            switch (itemType)
            {
                case ItemType.Life:
                    other.gameObject.GetComponent<PlayerRun>().RecoverLife(value);
                    FindAnyObjectByType<UIController>().UpdateLife();//UI更新
                    break;
                case ItemType.Bullet:
                    other.gameObject.GetComponent<PlayerShooter>().AddBullet(value);
                    FindAnyObjectByType<UIController>().UpdateBullet();//Bullet更新
                    break;
            }
            Destroy(gameObject);

        }
    }
}

/*
// 体力の回復・増加（Item.csから呼ばれる）
using UnityEngine;

public void AddLife(int value)
{
    life += value;

    // 先生の指示通り、最大値（DefaultLife = 3）以上にならないように制限する
    if (life > DefaultLife)
    {
        life = DefaultLife;
    }

    // UIControllerを探して、ライフ表示を更新する
    UIController controller = GameObject.FindAnyObjectByType<UIController>();
    if (controller != null)
    {
        controller.UpdateLife();
    }
}
*/