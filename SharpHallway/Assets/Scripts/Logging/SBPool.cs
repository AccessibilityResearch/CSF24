using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class SBPool {
	int maxSize = 10;
	ConcurrentBag<StringBuilder> pool;

	public SBPool(int initSize, int maxSize = 10) {
		pool = new ConcurrentBag<StringBuilder>();
		this.maxSize = maxSize;

		if (initSize > maxSize) { throw new ArgumentException("SBPool: initSize cannot be greater than maxSize."); }
		if (initSize < 1 || maxSize < 1) { throw new ArgumentException("SBPool: initSize and maxSize must both be greater than 0."); }

		for (int i = 0; i < initSize; i++) {
			pool.Add(new StringBuilder());
		}
	}

	public StringBuilder CheckOut() {
		if (pool.TryTake(out StringBuilder sb)) { return sb; }
		else { return new StringBuilder(); }
	}

	public void Return(StringBuilder sb) {
		if (sb == null) { throw new ArgumentNullException(nameof(sb), "SBPool: Cannot check in null string builder."); }
		if (pool.Count < maxSize) {
			sb.Clear();
			pool.Add(sb);
		}
	}
}
