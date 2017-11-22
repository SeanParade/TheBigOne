using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

		static private Player _instance;

		static public Player Instance
		{
			get
			{
				if (_instance == null) 
				{
					_instance = new Player ();
				}
				return _instance;
			}
		}



		private Player (){}

		public GameController gctl;

		private int _score = 0;
		private float _mass = 1;


		public int Score
		{
			get{ return _score; }
			set 
			{
				_score = value;
				gctl.updateUI ();
			}
		}

		public float Mass
		{
			get{ return _mass; }
			set
			{ 
				_mass = value;

			// Lose Condition: hit at 1 Mass
			    if (_mass < 1) 
				{
					gctl.gameOver ();
				} 
			// Win Condition: 5 mass collected
				else if (_mass >= 5)
				{
				    gctl.winner ();
				}
				else 
				{
					gctl.updateUI ();
				}

			}
		}
	}

