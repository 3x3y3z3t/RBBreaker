/*  GameClone/CCurrency.cs
 *  Version 1.0 (2025.05.31)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

namespace RBSaveEditor.GameClone
{
    public class CCurrency : ISaveable
    {
        public eCurrencyType CurrencyType => m_CurrencyType;
        public double Amount { get => m_Amount; set => m_Amount = value; }


        public CCurrency()
        { }

        public CCurrency(CCurrency _other)
        {
            m_CurrencyType = _other.m_CurrencyType;
            m_Amount = _other.m_Amount;
        }


        public void Load(SaveFileReader _reader)
        {
            m_CurrencyType = _reader.ReadEnum<eCurrencyType>();
            m_Amount = _reader.ReadDouble();
        }

        public void Save(SaveFileWriter _writer)
        {
            _writer.WriteEnum(m_CurrencyType);
            _writer.WriteDouble(m_Amount);
        }

        public override string ToString()
        {
            string str = "CCurrency (" + m_CurrencyType.ToString() + " x" + m_Amount.ToString("#,##0.##") + ")";
            return str;
        }


        public static CCurrency CreateAndLoad(SaveFileReader _reader)
        {
            CCurrency currency = new();
            currency.Load(_reader);
            return currency;
        }


        private eCurrencyType m_CurrencyType = eCurrencyType.Credits;
        private double m_Amount = 0.0;
    }

}
