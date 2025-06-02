/*  SaveFileReader.cs
 *  Version 1.0 (2025.06.01)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System.Reflection;

namespace RBSaveEditor
{
    public class SaveFileReader
    {
        //public enum ReadWriteableType
        //{
        //    Bool,

        //    Int32, Int64,

        //    Byte, Uint32, Uint64,

        //    String
        //}


        public SaveFileReader(string _filename)
        {
            FileStream fileStream = File.OpenRead(_filename);

            MemoryStream memoryStream = new((int)fileStream.Length);
            fileStream.CopyTo(memoryStream);
            fileStream.Close();

            memoryStream.Seek(0, SeekOrigin.Begin);
            m_Reader = new(memoryStream);
        }


        public void Close()
        {
            m_Reader.Close();
        }

        public override string ToString()
        {
            return string.Format("SaveFileReader (0x{0:x} | {1})", m_Reader.BaseStream.Position, m_Reader.BaseStream.Length);
        }

        #region Primitive Readers
        public bool ReadBool() => m_Reader.ReadBoolean();

        public int ReadInt32() => m_Reader.ReadInt32();
        public long ReadInt64() => m_Reader.ReadInt64();

        public byte ReadByte() => m_Reader.ReadByte();
        public uint ReadUInt32() => m_Reader.ReadUInt32();
        public ulong ReadUInt64() => m_Reader.ReadUInt64();

        public float ReadFloat() => m_Reader.ReadSingle();
        public double ReadDouble() => m_Reader.ReadDouble();

        public DateTime ReadDateTime() => new DateTime(m_Reader.ReadInt64());

        public byte[] ReadBytes(int _count) => m_Reader.ReadBytes(_count);

        public byte[] ReadToEnd()
        {
            long count = m_Reader.BaseStream.Length - m_Reader.BaseStream.Position;

            if (count <= int.MaxValue)
            {
                return m_Reader.ReadBytes((int)count);
            }

            // TODO: test this;
            List<byte> bytes = new();
            bytes.AddRange(m_Reader.ReadBytes(int.MaxValue));
            bytes.AddRange(ReadToEnd());

            return bytes.ToArray();
        }

        public string ReadString() => m_Reader.ReadString();
        #endregion

        #region Custom Readers
        public TEnum ReadEnum<TEnum>() where TEnum : Enum
        {
            Func<SaveFileReader, object> readFunc = GetOrComputeReadFuncFor<TEnum>();
            return (TEnum)readFunc(this);
        }

        public List<T> ReadList<T>() where T: notnull
        {
            Func<SaveFileReader, object> readFunc = GetOrComputeReadFuncFor<T>();

            _ = ReadByte();
            int count = ReadInt32();

            List<T> list = new(count);
            for (int i = 0; i < count; ++i)
            {
                T value = (T)readFunc(this);
                list.Add(value);
            }

            return list;
        }

        public HashSet<T> ReadHashSet<T>() where T : notnull
        {
            Func<SaveFileReader, object> readFunc = GetOrComputeReadFuncFor<T>();

            _ = ReadByte();
            int count = ReadInt32();

            HashSet<T> set = new(count);
            for (int i = 0; i < count; ++i)
            {
                T value = (T)readFunc(this);
                set.Add(value);
            }

            return set;
        }

        public Dictionary<TKey, TValue> ReadDictionary<TKey, TValue>() where TKey : notnull
        {
            Func<SaveFileReader, object> readKey = GetOrComputeReadFuncFor<TKey>();
            Func<SaveFileReader, object> readValue = GetOrComputeReadFuncFor<TValue>();

            _ = ReadByte();
            _ = ReadByte();
            int count = ReadInt32();

            Dictionary<TKey, TValue> dict = new(count);
            for (int i = 0; i < count; ++i)
            {
                TKey key = (TKey)readKey(this);
                TValue value = (TValue)readValue(this);
                dict.Add(key, value);
            }

            return dict;
        }
        #endregion

        //public TResult ReadCustom<TResult>(Func<SaveFileReader,TResult> _func)
        //{
        //    return _func(this);
        //}





        private static Func<SaveFileReader, object> GetOrComputeReadFuncFor<TResult>()
        {
            Type type = typeof(TResult);
            if (!s_CachedFunc.TryGetValue(type, out var func))
            {
                if (type.IsEnum)
                {
                    func = Local_GetReadFuncForUnderlyingType(type.GetEnumUnderlyingType());
                }
                else
                {
                    func = ComputeReadFunctionFor(type);
                }
                s_CachedFunc[type] = func;
            }

            return func;


            static Func<SaveFileReader, object> Local_GetReadFuncForUnderlyingType(Type _underlyingType)
            {
                if (!s_CachedFunc.TryGetValue(_underlyingType, out var func))
                    throw new InvalidOperationException("Cached Read method for primitive type '" + _underlyingType.Name + "' not found.");

                return func;
            }
        }


        private static Func<SaveFileReader, object> ComputeReadFunctionFor(Type _type)
        {
            MethodInfo? methodInfo = _type.GetMethod("Read", BindingFlags.Public | BindingFlags.Static, s_ReadMethodParams);
            if (methodInfo == null)
                throw new NotSupportedException("Type '" + _type.Name + "' is not supported (Supported types should implement static method '" + _type.Name + " Read(SaveFileReader)').");

            Func<SaveFileReader, object> readFunc = methodInfo.CreateDelegate<Func<SaveFileReader, object>>();
            return readFunc;
        }






        //private readonly MemoryStream m_Buffer;
        private readonly BinaryReader m_Reader;


        private static readonly Type[] s_ReadMethodParams = { typeof(SaveFileReader) };

        private static readonly Dictionary<Type, Func<SaveFileReader, object>> s_CachedFunc = new()
        {
            { typeof(bool), (_reader) => _reader.ReadBool() },

            { typeof(int), (_reader) => _reader.ReadInt32() },
            { typeof(long), (_reader) => _reader.ReadInt64() },

            { typeof(byte), (_reader) => _reader.ReadByte() },
            { typeof(uint), (_reader) => _reader.ReadUInt32() },
            { typeof(ulong), (_reader) => _reader.ReadUInt64() },

            { typeof(string), (_reader) => _reader.ReadString() },
        };
    }

}
