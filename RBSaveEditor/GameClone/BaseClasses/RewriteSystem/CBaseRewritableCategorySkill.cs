/*  GameClone/BaseClasses/RewriteSystem/CBaseRewritableCategorySkill.cs
 *  Version 2 (2025.06.04)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone.BaseClasses.RewriteSystem
{
    public abstract class CBaseRewritableCategorySkill : CBaseRewritableCategory
    {
        public eSkillRewriteType SkillRewriteType => m_SkillRewriteType;


        public override void Load(SaveFileReader _reader)
        {
            base.Load(_reader);

            m_SkillRewriteType = _reader.ReadEnum<eSkillRewriteType>();
        }

        public override void Save(SaveFileWriter _writer)
        {
            base.Save(_writer);

            _writer.WriteEnum(m_SkillRewriteType);
        }

        public override IDeepCloneable DeepClone()
        {
            var copy = (CBaseRewritableCategorySkill)base.DeepClone();
            return copy;
        }


        public static CBaseRewritableCategory? Create(eSkillRewriteType _rewriteType)
        {
            switch (_rewriteType)
            {
                //case eSkillRewriteType.Cooldown:
                //    return new CRewritableCategorySkillCooldown();
                //case eSkillRewriteType.MaxLevel:
                //    return new CRewritableCategorySkillMaxLevel();
                //case eSkillRewriteType.Duration:
                //    return new CRewritableCategorySkillDuration();
                //case eSkillRewriteType.Drawback:
                //    return new CRewritableCategorySkillDrawback();

                default:
                    Console.WriteLine("Invalid Skill Rewrite Type '" + _rewriteType + "'.");
                    return null;
            }
        }


        private eSkillRewriteType m_SkillRewriteType = eSkillRewriteType.Cooldown;
    }

}
