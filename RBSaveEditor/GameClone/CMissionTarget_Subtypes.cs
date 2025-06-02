/*  GameClone/CMissionTarget_Subtypes.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBSaveEditor.GameClone.BaseClasses;

namespace RBSaveEditor.GameClone
{
    public class CMissionTargetCharacter : CMissionTarget
    {
        public override void Load(SaveFileReader _reader)
        {
            m_PilotClass = _reader.ReadString();
            m_PilotName = _reader.ReadString();
            m_ShipFaction = _reader.ReadString();
            m_ShipName = _reader.ReadString();
        }

        public override void Save(SaveFileWriter _writer)
        {
            _writer.WriteString(m_PilotClass);
            _writer.WriteString(m_PilotName);
            _writer.WriteString(m_ShipFaction);
            _writer.WriteString(m_ShipName);
        }

        public override IDeepCloneable DeepClone() => base.DeepClone();


        private string m_PilotClass = string.Empty;
        private string m_PilotName = string.Empty;
        private string m_ShipFaction = string.Empty;
        private string m_ShipName = string.Empty;
    }

    public class CMissionTargetStructure : CMissionTarget
    {
        public override void Load(SaveFileReader _reader)
        {
            Console.WriteLine("> CMissionTarget_Subtypes.Load");

        }

        public override void Save(SaveFileWriter _writer)
        {
            Console.WriteLine("> CMissionTarget_Subtypes.Save");


        }

        public override IDeepCloneable DeepClone() => base.DeepClone();



    }

    public class CMissionTargetItem : CMissionTarget
    {
        public override void Load(SaveFileReader _reader)
        {
            Console.WriteLine("> CMissionTarget_Subtypes.Load");

        }

        public override void Save(SaveFileWriter _writer)
        {
            Console.WriteLine("> CMissionTarget_Subtypes.Save");


        }

        public override IDeepCloneable DeepClone() => base.DeepClone();



    }

}
