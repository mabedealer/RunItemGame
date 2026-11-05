using UnityEngine;

//自作の列挙型（ゲームのステータス）
public enum GameState
{
        None,
        IsPlay,
        GameClear,
        GameOver,
        GameEnd
}

public class GameManager : MonoBehaviour
{
    //自作した型を扱うStatic変数
    public static GameState gameState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState ==GameState.GameOver)
        {
            gameState = GameState.GameEnd;
        }
    }
}
