/*  SaveFileReader.cs
 *  Version 1.0 (2025.06.01)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RBSaveEditor
{
    public class SaveFileWriter
    {
        public SaveFileWriter()
        {
            m_Writer = new(new MemoryStream());
        }


        public void SaveFile(string _filename)
        {
            FileStream stream = File.OpenWrite(_filename);

            m_Writer.BaseStream.Seek(0, SeekOrigin.Begin);
            m_Writer.BaseStream.CopyTo(stream);
            stream.Close();
        }

        public void Close()
        {
            m_Writer.Close();
        }

        public override string ToString()
        {
            return string.Format("SaveFileWriter (0x{0:x} | {1})", m_Writer.BaseStream.Position, m_Writer.BaseStream.Length);
        }













        #region Primitive Writers
        public void WriteBool(bool _value) => m_Writer.Write(_value);

        public void WriteInt32(int _value) => m_Writer.Write(_value);
        public void WriteInt64(long _value) => m_Writer.Write(_value);

        public void WriteByte(byte _value) => m_Writer.Write(_value);
        public void WriteUInt32(uint _value) => m_Writer.Write(_value);
        public void WriteUInt64(ulong _value) => m_Writer.Write(_value);

        public void WriteFloat(float _value) => m_Writer.Write(_value);
        public void WriteDouble(double _value) => m_Writer.Write(_value);

        public void WriteDateTime(DateTime _value) => m_Writer.Write(_value.Ticks);

        public void WriteBytes(byte[] _value) => m_Writer.Write(_value);

        public void WriteString(string _value) => m_Writer.Write(_value);
        #endregion



        #region Custom Writers
        public void WriteEnum<TEnum>(TEnum _enum) where TEnum : Enum
        {
            Action<SaveFileWriter, object> writeFunc = GetOrComputeWriteFuncFor<TEnum>();
            writeFunc(this, _enum);
        }

        public void WriteList<T>(List<T> _list) where T : notnull
        {
            Action<SaveFileWriter, object> writeFunc = GetOrComputeWriteFuncFor<T>();

            WriteByte(0);
            WriteInt32(_list.Count);
            foreach (var item in _list)
            {
                writeFunc(this, item);
            }
        }

        public void WriteHashSet<T>(HashSet<T> _set) where T : notnull
        {
            Action<SaveFileWriter, object> writeFunc = GetOrComputeWriteFuncFor<T>();

            WriteByte(0);
            WriteInt32(_set.Count);
            foreach (var item in _set)
            {
                writeFunc(this, item);
            }
        }

        public void WriteDictionary<TKey, TValue>(Dictionary<TKey, TValue> _dict) where TKey : notnull where TValue : notnull
        {
            Action<SaveFileWriter, object> writeKey = GetOrComputeWriteFuncFor<TKey>();
            Action<SaveFileWriter, object> writeValue = GetOrComputeWriteFuncFor<TValue>();

            WriteByte(0);
            WriteByte(0);
            WriteInt32(_dict.Count);
            foreach (var pair in _dict)
            {
                writeKey(this, pair.Key);
                writeValue(this, pair.Value);
            }
        }
        #endregion




        private static Action<SaveFileWriter, object> GetOrComputeWriteFuncFor<T>()
        {
            Type type = typeof(T);

            if (!s_CachedFunc.TryGetValue(type, out var func))
            {
                if (type.IsEnum)
                {
                    func = Local_GetWriteFuncForUnderlyingType(type.GetEnumUnderlyingType());
                }
                else
                {
                    func = ComputeWriteFunctionFor(type);
                }
                s_CachedFunc[type] = func;
            }

            return func;


            static Action<SaveFileWriter, object> Local_GetWriteFuncForUnderlyingType(Type _underlyingType)
            {
                if (!s_CachedFunc.TryGetValue(_underlyingType, out var func))
                    throw new InvalidOperationException("Cached Write method for primitive type '" + _underlyingType.Name + "' not found.");

                return func;
            }
        }




        private static Action<SaveFileWriter, object> ComputeWriteFunctionFor(Type _type)
        {
            MethodInfo? methodInfo = _type.GetMethod("Write", BindingFlags.Public | BindingFlags.Static, s_WriteMethodParams);
            if (methodInfo == null)
                throw new NotSupportedException("Type '" + _type.Name + "' is not supported (Supported types should implement static method 'void Write(SaveFileWriter, " + _type.Name + ")').");

            Action<SaveFileWriter, object> writeFunc = methodInfo.CreateDelegate<Action<SaveFileWriter, object>>();
            return writeFunc;
        }






        private readonly MemoryStream m_Buffer;
        private readonly BinaryWriter m_Writer;


        private static readonly Type[] s_WriteMethodParams = { typeof(SaveFileWriter), typeof(object) };

        private static readonly Dictionary<Type, Action<SaveFileWriter, object>> s_CachedFunc = new()
        {
            { typeof(bool), (_writer, _value) => _writer.WriteBool((bool)_value) },

            { typeof(int), (_writer, _value) => _writer.WriteInt32((int)_value) },
            { typeof(long), (_writer, _value) => _writer.WriteInt64((long)_value) },

            { typeof(byte), (_writer, _value) => _writer.WriteByte((byte)_value) },
            { typeof(uint), (_writer, _value) => _writer.WriteUInt32((uint)_value) },
            { typeof(ulong), (_writer, _value) => _writer.WriteUInt64((ulong)_value) },

            { typeof(string), (_writer, _value) => _writer.WriteString((string)_value) },
        };
    }

}
