using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public Slider timerSlider;
	public Text timerText;
	public float gameTime;
	public bool stopTimer;
	float time;
	public Text litrosMensaje;
	public bool flagLTS = true;

	public bool generateRandom;

	public GameObject canvas;
	public GameObject canvasPerder;
	public GameObject particulas;

	public GameObject[] piecePrefabs;

	[System.Serializable]
	public class Puzzle
	{

		public int winValue;
		public int curValue;
		public int width;
		public int height;
		public Piece[,] pieces;


	}

	public Puzzle puzzle;



	void Start()
	{
		PlayerPrefs.SetInt("pipesSoundBool", 0);
		stopTimer = false;
		timerSlider.maxValue = gameTime;
		timerSlider.value = gameTime;


		canvas.SetActive(false);
		canvasPerder.SetActive(false);


		if (generateRandom)
		{

			GeneratePuzzle();


		}
		else
		{
			Vector2 dimensions = CheckDimensions();

			puzzle.width = (int)dimensions.x;
			puzzle.height = (int)dimensions.y;


			puzzle.pieces = new Piece[puzzle.width, puzzle.height];



			foreach (var piece in GameObject.FindGameObjectsWithTag("Piece"))
			{

				puzzle.pieces[(int)piece.transform.position.x, (int)piece.transform.position.y] = piece.GetComponent<Piece>();

			}
		}

		puzzle.winValue = GetWinValues();

		Shuffle();

		puzzle.curValue = Sweep();




	}

	public void soundActivePipes()
    {
		PlayerPrefs.SetInt("pipesSoundBool", 1);
	}

	public void Win()
	{

		canvas.SetActive(true);

		if (flagLTS)
		{
			flagLTS = false;
			int timelts = (int)time;
			int litrosTemp = PlayerPrefs.GetInt("litrosSum") + timelts;
			PlayerPrefs.SetInt("litrosSum", litrosTemp);
			litrosMensaje.text = timelts.ToString() + " litros";
			//Debug.Log(litrosTemp);
		}
				
	}
	public void Lose()
	{
		canvasPerder.SetActive(true);

	}

	void GeneratePuzzle()
	{

		puzzle.pieces = new Piece[puzzle.width, puzzle.height];
		int[] auxValues = { 0, 0, 0, 0 };

		for (int h = 0; h < puzzle.height; h++)
		{
			for (int w = 0; w < puzzle.width; w++)
			{

				//width restrictions
				if (w == 0)
				{
					auxValues[3] = 0;
				}
				else
				{
					auxValues[3] = puzzle.pieces[w - 1, h].values[1];
				}
				if (w == puzzle.width - 1)
				{
					auxValues[1] = 0;
				}
				else
				{
					auxValues[1] = Random.Range(0, 2);
				}

				//height restrictions
				if (h == 0)
				{
					auxValues[2] = 0;
				}
				else
				{
					auxValues[2] = puzzle.pieces[w, h - 1].values[0];
				}
				if (h == puzzle.height - 1)
				{
					auxValues[0] = 0;
				}
				else
				{
					auxValues[0] = Random.Range(0, 2);
				}


				//tells us piece type
				int valueSum = auxValues[0] + auxValues[1] + auxValues[2] + auxValues[3];


				if (valueSum == 2 && auxValues[0] != auxValues[2])
				{
					valueSum = 5;
				}

				GameObject go = (GameObject)Instantiate(piecePrefabs[valueSum], new Vector3(w, h, 0), Quaternion.identity);



				while (go.GetComponent<Piece>().values[0] != auxValues[0] ||
					  go.GetComponent<Piece>().values[1] != auxValues[1] ||
					  go.GetComponent<Piece>().values[2] != auxValues[2] ||
					  go.GetComponent<Piece>().values[3] != auxValues[3])

				{
					go.GetComponent<Piece>().RotatePiece();

				}

				puzzle.pieces[w, h] = go.GetComponent<Piece>();


			}
		}

	}

	public int Sweep()
	{
		int h;
		int w;
		int value1 = 0;


		for (h = 0; h < puzzle.height; h++)
		{
			for (w = 0; w < puzzle.width; w++)
			{

				//compares top
				if (h != puzzle.height - 1)
				{

					if (puzzle.pieces[w, h].values[0] == 1 && puzzle.pieces[w, h + 1].values[2] == 1)
					{
						value1++;
						//Debug.Log("Pase la primera suma");
					}
				}

				//compares right
				if (w != puzzle.width - 1)
				{
					if (puzzle.pieces[w, h].values[1] == 1 && puzzle.pieces[w + 1, h].values[3] == 1)
					{
						value1++;
						//Debug.Log("Pase la segunda suma");
					}
				}
			}

		}
		//Debug.Log("sume los valores");
		return value1;

	}

	public int QuickSweep(int w, int h)
	{


		int value = 0;

		//compares top
		if (h != puzzle.height - 1)
			if (puzzle.pieces[w, h].values[0] == 1 && puzzle.pieces[w, h + 1].values[2] == 1)
				value++;


		//compare right
		if (w != puzzle.width - 1)
			if (puzzle.pieces[w, h].values[1] == 1 && puzzle.pieces[w + 1, h].values[3] == 1)
				value++;


		//compare left
		if (w != 0)
			if (puzzle.pieces[w, h].values[3] == 1 && puzzle.pieces[w - 1, h].values[1] == 1)
				value++;

		//compare bottom
		if (h != 0)
			if (puzzle.pieces[w, h].values[2] == 1 && puzzle.pieces[w, h - 1].values[0] == 1)
				value++;


		return value;

	}

	int GetWinValues()
	{


		int winValue = 0;

		foreach (var piece in puzzle.pieces)
		{
			if (piece == null)
			{
				continue;
			}
			else
			{
				foreach (var j in piece.values)
				{
					winValue += j;
				}
			}

		}

		winValue /= 2;

		return winValue;

	}

	void Shuffle()
	{
		foreach (var piece in puzzle.pieces)
		{
			int k = Random.Range(0, 4);

			for (int i = 0; i < k; i++)
			{
				if (piece == null)
				{
					return;
				}
				else
				{
					piece.RotatePiece();
				}

			}
		}

	}


	Vector2 CheckDimensions()
	{
		Vector2 aux = Vector2.zero;

		GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

		foreach (var p in pieces)
		{
			if (p.transform.position.x > aux.x)
				aux.x = p.transform.position.x;

			if (p.transform.position.y > aux.y)
				aux.y = p.transform.position.y;
		}
		aux.x++;
		aux.y++;

		return aux;
	}


	public void NextLevel(string nextLevel)
	{
		PlayerPrefs.SetString("nextSceneName", nextLevel);
		//SceneManager.LoadScene(nextLevel);
		StartCoroutine(WaitWinCoroutine());
	}

	public void NextLevelLoseOption(string nextLevel)
	{
		PlayerPrefs.SetString("nextSceneName", nextLevel);
		WinLoadSceneMain();
	}


	public void Update()
	{
		time = gameTime - Time.timeSinceLevelLoad;

		int minutes = Mathf.FloorToInt(time / 60);
		int seconds = Mathf.FloorToInt(time - minutes * 60);

		string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);

		if (time <= 0)
		{
			stopTimer = true;

		}
		if (puzzle.curValue == puzzle.winValue)
		{
			stopTimer = true;
		}

		if (stopTimer == false)
		{
			timerText.text = textTime;
			timerSlider.value = time;

		}
	}

	IEnumerator WaitWinCoroutine()
	{
		//yield on a new YieldInstruction that waits for 5 seconds.
		yield return new WaitForSeconds(3);
		WinLoadSceneMain();
	}

	public void WinLoadSceneMain()
	{
		SceneManager.LoadScene("MultiLoader");
	}



}