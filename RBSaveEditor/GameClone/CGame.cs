/*  GameClone/CGame.cs
 *  Version 1.0 (2025.05.30)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSaveEditor.GameClone
{
    public class CGame
    {
        public const int CURRENT_VERSION_NUMBER = 110;


        public int VersionNumber => m_VersionNumber;
        public DateTime SaveDateTime => m_SaveDateTime;

        // profile type

        public CPlayer Player => m_Player;










        public CMetagame? Metagame => m_Metagame;



        public CGame()
        { }

        public CGame(CGame _other)
        {
            m_VersionNumber = _other.VersionNumber;
            m_SaveDateTime = _other.SaveDateTime;












        }



            public bool LoadMetagame(string _fullname)
        {
            SaveFileReader reader;
            try
            {
                reader = new(_fullname);
            }
            catch (Exception _ex)
            {
                Console.WriteLine("LoadMetagame(): Could not read metagame save file: " + _ex);
                return false;
            }

            m_Metagame = new();
            bool flag = m_Metagame.Load(reader);
            reader.Close();

            if (flag)
            {
                return true;
            }

            Console.WriteLine("LoadMetagame(): Could not read metagame from file.");
            m_Metagame = null;
            return false;
        }

        public bool LoadProfile(SaveFileReader _reader)
        {
            m_VersionNumber = _reader.ReadInt32();
            m_SaveDateTime = DateTime.FromBinary(_reader.ReadInt64());

            m_ProfileType = _reader.ReadEnum<eProfileType>();

            m_Player = new(_isHumanPlayer: true);
            m_Player.Load(_reader);

            Console.WriteLine("> CGame.LoadProfile " + _reader.ToString());
            //m_StoryManager.Load(_reader);







            return true;
        }


        public bool SaveMetagame(string _fullname)
        {
            if (m_Metagame == null)
            {
                Console.WriteLine("SaveMetagame(): m_Metagame is null.");
                return false;
            }

            SaveFileWriter writer = new();

            bool flag = m_Metagame.Save(writer);

            writer.SaveFile(_fullname);
            writer.Close();

            if (flag)
            {
                return true;
            }

            Console.WriteLine("SaveMetagame(): Could not write metagame to file.");
            m_Metagame = null;
            return false;
        }

        public bool SaveProfile(SaveFileWriter _writer)
        {
            _writer.WriteInt32(m_VersionNumber);
            _writer.WriteInt64(m_SaveDateTime.ToBinary());

            _writer.WriteEnum(m_ProfileType);

            m_Player.Save(_writer);

            Console.WriteLine("> CGame.SaveProfile");
            //m_StoryManager.Save(_writer);









            return false;
        }






        private int m_VersionNumber = CGame.CURRENT_VERSION_NUMBER;
        private DateTime m_SaveDateTime;

        private eProfileType m_ProfileType = eProfileType.Unknown;

        private CPlayer m_Player = null!;









        private CMetagame? m_Metagame;
    }

}
