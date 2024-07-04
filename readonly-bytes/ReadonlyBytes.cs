using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private bool initialized = true;
        private int hash1; 
        private byte[] ArrayBytes { get; set; }

        internal int Length { get; private set; }

        private void InitWithArray(IEnumerable<byte> arr, bool wasOneArgument)
        {
            this.ArrayBytes = arr.ToArray();
            this.Length = arr.Count();
            if (wasOneArgument)
                initialized = false;
        }

        public ReadonlyBytes(params object[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException();
            if (keys.Length == 1 && keys[0] is IEnumerable<byte>)
            {
                var arr = keys[0] as IEnumerable<byte>;
                if (arr == null)
                    throw new ArgumentNullException();
                InitWithArray(arr, true);
                hash1 = FirstGetHashCode();
            }
            else
            {
                var Length = keys.Length;
                ArrayBytes = new byte[Length];
                for (int i = 0; i < Length; i++)
                    ArrayBytes[i] = byte.Parse(keys[i].ToString());
                InitWithArray(ArrayBytes, false);
                hash1 = FirstGetHashCode();
            }
        }

        public int this[int index]
        {
            get { return ArrayBytes[index]; }
        }

        public override bool Equals(object obj)
        {
            var array = obj as ReadonlyBytes;
            if (array == null)
                return false;
            return hash1 == array.GetHashCode();
        }


        public override int GetHashCode()
        {
            return hash1;
        }
        
        public int FirstGetHashCode()
        {
            int hash = unchecked((int)2166136261);
            int prime = 16777619;
            var sha = SHA512.Create();
            var bytes = sha.ComputeHash(ArrayBytes);
            if (initialized)
                hash += 197;
            for (int i = 0; i < ArrayBytes.Length; i++)
                hash = unchecked((int)(hash ^ ArrayBytes[i]) * prime);
            return hash;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return ArrayBytes[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string res = "[";
            foreach (var item in ArrayBytes)
                res += item.ToString() +", ";
            if (res.Length > 2 &&  res[res.Length - 2] == ',')
                res = res.Substring(0, res.Length - 2);
            res += "]";
            return res;
        }
    }
}