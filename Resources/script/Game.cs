using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private enum State { play, over };

    public Text ScoreUI;
    public Text GameOverUI;
    public Text GameOverMessageUI;

    private string gameoverui_text = "GameOver";
    private string gameovermessage_text = "press space key";

    private int hiscore = 0;
    private int score = 0;
    private State state = State.play;

	void Start ()
    {
        GameOverUI.text = "";
        GameOverMessageUI.text = "";
        Puzzle.Init();
	}
	void Update ()
    {
        ScoreUpdate();

        if (Puzzle.IsGameOver() || state == State.over)
        {
            state = State.over;
            GameOver();
        }

        else if (state == State.play)
        {
            Drow.Clear();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Puzzle.IsMove("Left")) Puzzle.Drop();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Puzzle.IsMove("Right")) Puzzle.Drop();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Puzzle.IsMove("Up")) Puzzle.Drop();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Puzzle.IsMove("Down")) Puzzle.Drop();
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            Drow.Clear();
            Drow.DrowTable();
        }
	}

    void ScoreUpdate()
    {
        int margin = Puzzle.Score() - score;

        if (margin == 0) return;
        if (margin >= 1000) score += 200;
        else if (margin >= 500) score += 100;
        else if (margin >= 100) score += 20;
        else if (margin >= 50) score += 10;
        else ++score;

        if (hiscore <= score) hiscore = score;

        ScoreUI.text = string.Format("{0:D6}", score);
    }


    void GameOver()
    {
        if (state != State.over) return;

        GameOverUI.text = gameoverui_text;
        GameOverMessageUI.text = gameovermessage_text;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.play;
            GameOverUI.text = "";
            GameOverMessageUI.text = "";
            Puzzle.Init();
            score = 0;
            Puzzle.ScoreReset();
        }
    }
}
