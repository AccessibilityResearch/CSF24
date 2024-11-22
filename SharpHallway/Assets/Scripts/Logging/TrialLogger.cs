using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System;

public class TrialLogger : IDisposable {
	//We can get both player position and rotation from the head.
	[SerializeField] private Transform subject;

	private float startTime;
	private readonly string path;

	private StreamWriter writer;

	//Memory stuffs
	private readonly int flushInterval = 100; //So we aren't using I/O every f* frame

	//Logging "itterables"
	private int frameCount = 0; //What he said ^

	private List<string> buffer = new List<string>();
	private List<string> collisions = new List<string>();
	private SBPool pool = new SBPool(10);

	bool disposed;


	public TrialLogger(Transform _subject, string _path, int _flushInterval, int _bufferSize, Encoding _encoding) {
		subject = _subject;
		flushInterval = _flushInterval;
		path = _path;

		if (!Directory.Exists(Path.GetDirectoryName(_path))) {
			Directory.CreateDirectory(Path.GetDirectoryName(_path));
		}

		writer = new StreamWriter(_path, true, _encoding, _bufferSize);
		writer.WriteLine("Time,PlayerX,PlayerY,Pitch,Yaw,Collisions");
		writer.Flush();

		startTime = Time.time;
	}

	public TrialLogger(Transform _subject, string _path, int _flushInterval = 100, int _bufferSize = 8192)
		: this(_subject, _path, _flushInterval, _bufferSize, Encoding.UTF8) { }


	public void WriteLog() {
		if (disposed) { throw new ObjectDisposedException($"Attempting to call methods on disposed class\n{path}"); }

		StringBuilder sb = pool.CheckOut(); //Take a stringbuilder from the string builder pool -- this is mostly for multithreading which isn't going to happen lol
		sb.Clear(); //just in case

		sb.AppendFormat("{0:F3},{1:F3},{2:F3},{3:F3},{4:F3}",
			Time.time - startTime,
			subject.position.x,
			subject.position.z,
			subject.eulerAngles.y,
			subject.eulerAngles.z
			);

		sb.Append(','); //yep a whole new line to avoid string concat, what a world we live in.
		sb.Append((collisions.Count > 0) ? string.Join("|", collisions) : "None"); //If there are any collisions that interval, log em'

		buffer.Add(sb.ToString());

		collisions.Clear();
		pool.Return(sb);

		//Check to see if its time to write to file
		frameCount++;
		if (frameCount > flushInterval) {
			FlushBuffer();
			frameCount = 0;
		}
	}

	private void FlushBuffer() {
		if (disposed) { throw new ObjectDisposedException($"Attempting to call methods on disposed class\n{path}"); }

		if (buffer.Count > 0) {
			foreach (string log in buffer) {
				writer.WriteLine(log);
			}

			buffer.Clear();
			writer.Flush();
		}
	}

	public void LogCollision(GameObject go) {
		if (disposed) { throw new ObjectDisposedException($"Attempting to call methods on disposed class\n{path}"); }

		collisions.Add(go.name);
	}

	public void OnApplicationQuit() {
		Dispose();
	}

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing) {
		if (!disposed) {
			if (disposing) {
				writer?.Flush();
				writer?.Close();
				writer?.Dispose();
			}

			disposed = true;
		}
	}

	~TrialLogger() {
		Dispose(false);
	}
}
