/*  GameClone/CProgressionElement.cs
 *  Version 1.0 (2025.06.03)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CProgressionElement : CElement, ISaveable
    {
        public int Level { get => m_Level; set => m_Level = value; }
        public double Experience { get => m_Experience; set => m_Experience = value; }


        public virtual void Load(SaveFileReader _reader)
        {
            m_Level = _reader.ReadInt32();
            m_Experience = _reader.ReadDouble();

            int count = _reader.ReadInt32();
            m_MapProgresses = new(count);
            for (int i = 0; i < count; ++i)
            {
                string key = _reader.ReadString();
                CMapProgress mapProgress = new();
                mapProgress.Load(_reader);

                m_MapProgresses.Add(key, mapProgress);
            }
        }

        public virtual void Save(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_Level);
            _writer.WriteDouble(m_Experience);

            _writer.WriteInt32(m_MapProgresses.Count);
            foreach(var pair in m_MapProgresses)
            {
                _writer.WriteString(pair.Key);
                pair.Value.Save(_writer);
            }
        }

        public override IDeepCloneable DeepClone()
        {
            var copy = (CProgressionElement)MemberwiseClone();
            copy.m_MapProgresses = m_MapProgresses.DeepClone();

            return copy;
        }


        public static CProgressionElement Create(string _protoName)
        {
            switch (_protoName)
            {
                case Constants.Proto.Player:
                    return new CProgressionElementPlayer();
                    // TODO: more progression elements;
                default:
                    throw new ArgumentException("Invalid proto name '" + _protoName + "'.");
            }
        }


        protected int m_Level = -1;
        protected double m_Experience = 0.0;

        private Dictionary<string, CMapProgress> m_MapProgresses = null!;
    }

}
