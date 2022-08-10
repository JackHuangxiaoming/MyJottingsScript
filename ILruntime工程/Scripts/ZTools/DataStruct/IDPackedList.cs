using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZTools.DataStructures
{
    public sealed class IDPackedList<TKey, T> where TKey : System.IComparable<TKey> where T : System.IEquatable<T>
    {
        private List<KeyValuePair<TKey, T>> internal_list;

        public IDPackedList(int _capacity)
        {
            internal_list = new List<KeyValuePair<TKey, T>>();
        }

        public void Add(TKey _key, T _value)
        {
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                if (internal_list[i].Key.CompareTo(_key) > 0)
                {
                    //插入
                    internal_list.Insert(i, new KeyValuePair<TKey, T>(_key, _value));
                    return;
                }
            }

            internal_list.Add(new KeyValuePair<TKey, T>(_key, _value));
        }

        public int Find(TKey _key, T _value)
        {
            return internal_list.FindIndex(0, (v) => { return v.Key.CompareTo(_key) == 0 && v.Value.Equals(_value); });
        }

        public void Find(T _value, List<int> _resultBuffer)
        {
            int startIndex = 0;
            while (true)
            {
                var current = internal_list.FindIndex(startIndex, (v) => { return v.Value.Equals(_value); });
                if (current == -1) break;
                else
                {
                    _resultBuffer.Add(current);
                    startIndex = current + 1;
                }
            }
        }
        
        public bool Find(TKey _key, out int _startIndex, out int _count)
        {
            int index = -1;
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                var v = internal_list[i];
                if (v.Key.CompareTo(_key) == 0)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                int countOfKey = 0;

                for (int i = index, count = internal_list.Count; i < count; ++i)
                {
                    var v = internal_list[i];
                    if (v.Key.CompareTo(_key) == 0)
                        ++countOfKey;
                    else
                        break;
                }

                _startIndex = index;
                _count = countOfKey;
                return true;
            }

            _startIndex = -1;
            _count = 0;
            return false;
        }

        public T FindFirstOne(TKey _key)
        {
            var index = internal_list.FindIndex(0, (v) => { return v.Key.CompareTo(_key) == 0; });
            if (index == -1)
                return default(T);
            else
                return internal_list[index].Value;
        }

        public void FindAllValuesOfKey(TKey _key, List<T> _resultBuffer)
        {
            int startIndex = 0;

            while (true)
            {
                if (startIndex >= internal_list.Count) break;

                var current = internal_list.FindIndex(startIndex, (v) => { return v.Key.Equals(_key); });
                if (current == -1) break;
                else
                {
                    _resultBuffer.Add(internal_list[current].Value);
                    startIndex = current + 1;
                }
            }
        }

        public bool Remove(TKey _key, T _value)
        {
            var index = Find(_key, _value);
            if (index != -1)
            {
                internal_list.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove(T _value)
        {
            int startIndex = 0;
            while (true)
            {
                var current = internal_list.FindIndex(startIndex, (v) => { return v.Value.Equals(_value); });
                if (current == -1) break;
                else
                {
                    internal_list.RemoveAt(current);
                    startIndex = current;
                }
            }
        }
        
        public void RemoveRange(int _index, int _count)
        {
            internal_list.RemoveRange(_index, _count);
        }

        public void Clear()
        {
            internal_list.Clear();
        }

        public T this[int _index] { get { return internal_list[_index].Value; } }

        public int Count { get { return internal_list.Count; } }
    }

    public sealed class IDPackedObjectList<TKey, T> where TKey : System.IComparable<TKey> where T : class
    {
        private List<KeyValuePair<TKey, T>> internal_list;

        public IDPackedObjectList(int _capacity)
        {
            internal_list = new List<KeyValuePair<TKey, T>>();
        }

        public void Add(TKey _key, T _value)
        {
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                if (internal_list[i].Key.CompareTo(_key) > 0)
                {
                    //插入
                    internal_list.Insert(i, new KeyValuePair<TKey, T>(_key, _value));
                    return;
                }
            }

            internal_list.Add(new KeyValuePair<TKey, T>(_key, _value));
        }

        public int Find(TKey _key, T _value)
        {
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                var v = internal_list[i];
                if (v.Key.CompareTo(_key) == 0 && v.Value == _value)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Find(TKey _key, out int _startIndex, out int _count)
        {
            int index = -1;
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                var v = internal_list[i];
                if (v.Key.CompareTo(_key) == 0)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                int countOfKey = 0;

                for (int i = index, count = internal_list.Count; i < count; ++i)
                {
                    var v = internal_list[i];
                    if (v.Key.CompareTo(_key) == 0)
                        ++countOfKey;
                    else
                        break;
                }

                _startIndex = index;
                _count = countOfKey;
                return true;
            }

            _startIndex = -1;
            _count = 0;
            return false;
        }

        public void Find(T _value, List<int> _resultBuffer)
        {
            int startIndex = 0;
            while (true)
            {
                var current = internal_list.FindIndex(startIndex, (v) => { return v.Value == _value; });
                if (current == -1) break;
                else
                {
                    _resultBuffer.Add(current);
                    startIndex = current + 1;
                }
            }
        }

        public void FindAllValuesOfKey(TKey _key, List<T> _resultBuffer)
        {
            int startIndex = 0;
            while (true)
            {
                if (startIndex >= internal_list.Count) break;

                var current = internal_list.FindIndex(startIndex, (v) => { return v.Key.Equals(_key); });
                if (current == -1) break;
                else
                {
                    _resultBuffer.Add(internal_list[current].Value);
                    startIndex = current + 1;
                }
            }
        }

        public T FindFirstOne(TKey _key)
        {
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                var v = internal_list[i];
                if (v.Key.CompareTo(_key) == 0)
                {
                    return v.Value;
                }
            }

            return default;
        }

        public int FindFirstOneIndex(TKey _key)
        {
            for (int i = 0, count = internal_list.Count; i < count; ++i)
            {
                var v = internal_list[i];
                if (v.Key.CompareTo(_key) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool RemoveKey(TKey _key)
        {
            if (Find(_key, out var index, out var count))
            {
                internal_list.RemoveRange(index, count);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveRange(int _index, int _count)
        {
            internal_list.RemoveRange(_index, _count);
        }

        public bool Remove(TKey _key, T _value)
        {
            var index = Find(_key, _value);
            if (index != -1)
            {
                internal_list.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove(T _value)
        {
            int startIndex = 0;
            while (true)
            {
                var current = internal_list.FindIndex(startIndex, (v) => { return v.Value == _value; });
                if (current == -1) break;
                else
                {
                    internal_list.RemoveAt(current);
                    startIndex = current;
                }
            }
        }

        public void Clear()
        {
            internal_list.Clear();
        }

        public T this[int _index] { get { return internal_list[_index].Value; } }

        public TKey GetKey(int _index) { return internal_list[_index].Key; }

        public KeyValuePair<TKey, T> Get(int _index) { return internal_list[_index]; }

        public int Count { get { return internal_list.Count; } }
    }
}