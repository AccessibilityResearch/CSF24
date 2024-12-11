using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Buffers.Text;
using System.Linq;

public class LogManager : MonoBehaviour {
	//Looking at this code has given me a small handful of diseases but at least it works :)  -- It's mostly the naming schemes and the orginization of the program (TransformSpy should not be the logging manager [hey thats a good file name!])
	//I am starting to like it more.

	private const bool enableLogging = true;

	//So many static vars :/
	private static LogManager instance;
	public static LogManager Instance { get { return instance; } private set { instance = value; } }
	public TrialLogger trialLogger { get; private set; }

	private static string experimentID;
	private string basePath;
	private static int trialNum = 0;

	public static string ExperimentID { get { return experimentID; } }
	public static int TrialNum { get { return trialNum; } private set { trialNum = value; } }

	public void Awake() {
		Instance = this;
	}

	public void Start()
	{
		if (enableLogging && TransformSpy.Body == null) { throw new MissingReferenceException("LogManager: TransformSpy must be attached to player camera for logging to occur."); }
	}

	public void FixedUpdate() {
		if (enableLogging && trialLogger != null) {
			trialLogger.WriteLog();
		}
	}

	public void LogCollision(string name) {
		if(enableLogging && trialLogger == null) { throw new MissingReferenceException("LogManager: TrialLogger not created."); }
		trialLogger.LogCollision(name);
	}

	public void NewTrial() {
		if(enableLogging) {
			//This handles everything, if it's the start of an expirement we create a new folder to hold the trial logs.
			//this also keeps track of the trial we are currently on (this may get delegated to another script so the expirements will actuall end)
			//If a trail logger already exists (will most of the time), it needs to be disposed (free its memory cuz stream writer)

			//Finally just make a new logger and tell it to start (probably unessary) yes I know I can't spell.

			Debug.Log($"New Trial #{TrialNum}");
			if(TrialNum++ == 0) { CreateBasePath(); Debug.Log("New Base Path"); }

			if(TransformSpy.Body == null) { throw new MissingReferenceException("LogManager: TransformSpy must be attached to player camera for logging to occur."); }
			if(trialLogger != null) { trialLogger.Dispose(); }

			trialLogger = new TrialLogger(TransformSpy.Transfourm, Path.Combine(basePath, $"{TrialNum}.csv"));
			trialLogger.Start();
		}
	}

	private void CreateBasePath() {
		//Does what you think it does, Path.Combine accounts for os differences in directories.

		experimentID = GenerateExperimentID();
		basePath = Path.Combine(Application.persistentDataPath, "Experiments", experimentID);
		string directoryPath = Path.GetDirectoryName(basePath);

		if (Directory.Exists(directoryPath)) {
			Directory.CreateDirectory(directoryPath);
		}
	}

	public string GenerateExperimentID() {
		//Really really basic encryption? (Not sure if this is even enough to be considered encryption)
		//Feel free to play around with this, the key can be whatever and the other number just needs to be a large enough prime.

		long uuid = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		long key = 49979687; //changed this to a prime

		uuid = (uuid % 1000000007) ^ key;

		return Base36(uuid);
	}

	private string Base36(long num) {
		//Takes number and converts it into base 36, the same as any other base.
		//Same with the encryption method, feel free to change this to do another base. All you need to do is change both 36 to whatever base you want.
		//Just remember that if you go above 36 you will need to add more characters to the 'chars' string.

		const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		StringBuilder sb = new StringBuilder();

		bool isNeg = num < 0;
		num = Math.Abs(num);

		//do while
		do {
			sb.Append(chars[(int)(num % 36)]);
			num /= 36;
		} while (num > 0);

		return sb.ToString();
	}
}
