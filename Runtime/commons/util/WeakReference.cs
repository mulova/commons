﻿//----------------------------------------------
// Unity3D common libraries and editor tools
// License: The MIT License ( http://opensource.org/licenses/MIT )
// Copyright © 2013- mulova@gmail.com
//----------------------------------------------

using System;
using System.Collections.Generic;

// Adds strong typing to WeakReference.Target using generics. Also,
// the Create factory method is used in place of a constructor
// to handle the case where target is null, but we want the 
// reference to still appear to be alive.

namespace mulova.commons
{
	public class WeakReference<T> : WeakReference where T : class {
		private T strongRef;
		
		public static WeakReference<T> Create(T target) {
			return Create(target, false);
		}
		
		public static WeakReference<T> Create(T target, bool strong) {
			if (target == null)
				return WeakNullReference<T>.Singleton;
			
			WeakReference<T> r = new WeakReference<T>(target);
			r.Strong = strong;
			return r;
		}
		
		protected WeakReference(T target)
			: base(target, false) { }
		
		public new T Target {
			get { return (T)base.Target; }
		}
		
		public bool Strong {
			get { return strongRef != null; }
			set {
				if (value) {
					strongRef = Target;
				} else {
					strongRef = null;
				}
			}
		}
	}

	// Provides a weak reference to a null target object, which, unlike
	// other weak references, is always considered to be alive. This 
	// facilitates handling null dictionary values, which are perfectly
	// legal.
	public class WeakNullReference<T> : WeakReference<T> where T : class {
		public static readonly WeakNullReference<T> Singleton = new WeakNullReference<T>();

		private WeakNullReference() : base(null) { }

		public override bool IsAlive {
			get { return true; }
		}
	}

	// Provides a weak reference to an object of the given type to be used in
	// a WeakDictionary along with the given comparer.
	public sealed class WeakKeyReference<T> : WeakReference<T> where T : class {
		public readonly int HashCode;

		public WeakKeyReference(T key, WeakKeyComparer<T> comparer)
			: base(key) {
			// retain the object's hash code immediately so that even
			// if the target is GC'ed we will be able to find and
			// remove the dead weak reference.
			this.HashCode = comparer.GetHashCode(key);
		}
	}

	// Compares objects of the given type or WeakKeyReferences to them
	// for equality based on the given comparer. Note that we can only
	// implement IEqualityComparer<T> for T = object as there is no 
	// other common base between T and WeakKeyReference<T>. We need a
	// single comparer to handle both types because we don't want to
	// allocate a new weak reference for every lookup.
	public sealed class WeakKeyComparer<T> : IEqualityComparer<object>
		where T : class {

		private IEqualityComparer<T> comparer;

		public WeakKeyComparer(IEqualityComparer<T> comparer) {
			if (comparer == null)
				comparer = EqualityComparer<T>.Default;

			this.comparer = comparer;
		}

		public int GetHashCode(object obj) {
			WeakKeyReference<T> weakKey = obj as WeakKeyReference<T>;
			if (weakKey != null) return weakKey.HashCode;
			return this.comparer.GetHashCode((T)obj);
		}

		// Note: There are actually 9 cases to handle here.
		//
		//  Let Wa = Alive Weak Reference
		//  Let Wd = Dead Weak Reference
		//  Let S  = Strong Reference
		//  
		//  x  | y  | Equals(x,y)
		// -------------------------------------------------
		//  Wa | Wa | comparer.Equals(x.Target, y.Target) 
		//  Wa | Wd | false
		//  Wa | S  | comparer.Equals(x.Target, y)
		//  Wd | Wa | false
		//  Wd | Wd | x == y
		//  Wd | S  | false
		//  S  | Wa | comparer.Equals(x, y.Target)
		//  S  | Wd | false
		//  S  | S  | comparer.Equals(x, y)
		// -------------------------------------------------
		public new bool Equals(object x, object y) {
			bool xIsDead, yIsDead;
			T first  = GetTarget(x, out xIsDead);
			T second = GetTarget(y, out yIsDead);

			if (xIsDead)
				return yIsDead ? x == y : false;

			if (yIsDead)
				return false;

			return this.comparer.Equals(first, second);
		}

		private static T GetTarget(object obj, out bool isDead) {
			WeakKeyReference<T> wref = obj as WeakKeyReference<T>;
			T target;
			if (wref != null) {
				target = wref.Target;
				isDead = !wref.IsAlive;
			}
			else {
				target = (T)obj;
				isDead = false;
			}
			return target;
		}
	}

}

