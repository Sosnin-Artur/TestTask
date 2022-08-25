using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoroutineWrapper
{
    public class CoroutineObject : BaseCoroutineObject
    {
        public CoroutineObject(
            MonoBehaviour owner,        
            Func<IEnumerator> routine) : base(owner, routine)     
        {
        }

        public override void Start()
        {
            Stop();

            Coroutine = Owner.StartCoroutine(Process());
        }        

        public override void Stop()
        {
            if (IsProcessing)
            {
                Owner.StopCoroutine(Coroutine);

                Coroutine = null;
            }
        }

        private IEnumerator Process()
        {
            yield return Routine.Invoke();

            Coroutine = null;
        }
    }
}