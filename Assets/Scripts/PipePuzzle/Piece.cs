using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

	public int[] values;
	public float speed;
	float realRotation;
	bool flagWin = true;

	public GameManager gm;



	void Start()
	{
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	void Update()
	{


		if (transform.root.eulerAngles.z != realRotation)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
		}

		if (gm.puzzle.curValue == gm.puzzle.winValue && flagWin)
		{
			flagWin = false;
			gm.Win();
			gm.particulas.SetActive(false);
		}
		if (gm.canvas.activeInHierarchy == false)
		{
			if (gm.stopTimer == true && gm.puzzle.curValue != gm.puzzle.winValue)
			{
				gm.Lose();
			}
		}

	}



	void OnMouseDown()
	{

		int diff = -gm.QuickSweep((int)transform.position.x, (int)transform.position.y);

		if (GameObject.FindGameObjectWithTag("Canvas") == false)
		{
			if (gm.puzzle.winValue != gm.puzzle.curValue)
			{
				RotatePiece();
			}
		}
		diff += gm.QuickSweep((int)transform.position.x, (int)transform.position.y);

		gm.puzzle.curValue += diff;


	}

	public void RotatePiece()
	{
		
		realRotation += 90;

		if (realRotation == 360)
			realRotation = 0;

		RotateValues();
	}





	public void RotateValues()
	{
		int temp;
		temp = values[0];
		for (int i = 0; i < 3; i++)
		{
			values[i] = values[i + 1];
		}
		values[3] = temp;
		//Debug.Log("tumama");
	}




}