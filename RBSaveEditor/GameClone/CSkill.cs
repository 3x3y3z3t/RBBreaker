/*  GameClone/CSkill.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using RBSaveEditor.GameClone.BaseClasses.RewriteSystem;

namespace RBSaveEditor.GameClone
{
    public class CSkill : ISaveable
    {
        public string Proto_Name => m_Proto_Name;
        public bool Proto_Deprecated => m_Proto_Deprecated;


        public CSkill(CPlayer _ownerPlayer)
        {
            m_OwnerPlayer = _ownerPlayer;
        }


        public void Load(SaveFileReader _reader)
        {
            m_GameplayRandomSeed = _reader.ReadInt32();

            m_Level = _reader.ReadInt32();

            m_Locked = _reader.ReadBool();

            {
                int count = _reader.ReadInt32();
                m_RewritableCategories = new(count);
                for (int i = 0; i < count; ++i)
                {
                    eSkillRewriteType rewriteType = _reader.ReadEnum<eSkillRewriteType>();
                    CBaseRewritableCategory? category = CBaseRewritableCategorySkill.Create(rewriteType);
                    if (category == null)
                    {
                        throw new ArgumentException("Couldn't create Rewritable Category Skill for type '" + rewriteType + "'.");
                    }

                    category.Load(_reader);
                    m_RewritableCategories.Add(category);
                }
            }
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteString(m_Proto_Name); // read in CSkill.CreateAndLoad();

            _writer.WriteInt32(m_GameplayRandomSeed);

            _writer.WriteInt32(m_Level);

            _writer.WriteBool(m_Locked);

            _writer.WriteInt32(m_RewritableCategories.Count);
            foreach (CBaseRewritableCategory category in m_RewritableCategories)
            {
                if (category is not CBaseRewritableCategorySkill categorySkill)
                {
                    throw new ArgumentException("Rewritable Category is not a CBaseRewritableCategorySkill.");
                }

                _writer.WriteEnum(categorySkill.SkillRewriteType);
                category.Save(_writer);
                //categorySkill.Save(_writer);
            }
        }


        public static CSkill? CreateAndLoad(CPlayer _ownerPlayer, SaveFileReader _reader)
        {
            string name = _reader.ReadString();

            bool shouldIgnoreSkill = false;

            //CSkillProto? proto = _ownerPlayer.GetPlayerClass().GetSkillProto(name);
            //if (proto == null)
            //{
            //    proto = new();
            //    shouldIgnoreSkill = true;
            //}

            CSkill skill = new(_ownerPlayer);
            //skill.m_Proto = proto;
            skill.m_Proto_Name = name;
            skill.Load(_reader);

            if (shouldIgnoreSkill)
            {
                return null;
            }

            return skill;
        }


        private string m_Proto_Name = string.Empty;
        private bool m_Proto_Deprecated = false; // TODO: actually load Deprecated information;

        private int m_GameplayRandomSeed = 0;

        private int m_Level = 0;

        private bool m_Locked = false;

        private List<CBaseRewritableCategory> m_RewritableCategories = null!;

        private CPlayer m_OwnerPlayer = null!;
    }

}
