
public enum PromoCodeTypes
{
    NONE,
    XP_GAIN,
    MONEY_GAIN,
    MONEY_MULTIPLIER
    //APPEARANCE
}

public class EntityPromoCodes
{
    private static readonly string ValidPromoCodeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
    //private static readonly string ValidPromoCodeWithSpecialCharacters = "!@#$%^&*()_,.?;:ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

    private string promoCodeType = "";
    private string promoCode = "";
    private bool isUsed = false;
    private double promoCodeAction = 0;

    //public string PromoCode { get => ; set; }
    //public string PromoCode { get => promoCode; set => promoCode = value; }
    public string PromoCodeType { get => promoCodeType; set => promoCodeType = ((PromoCodeTypes)RNG.GetInstance().Next(1, 4)).ToString(); }

    public string PromoCode
    {
        get { return promoCode; }
        set
        {
            for (int j = 0; j < 12; j++)
                promoCode += ValidPromoCodeCharacters.ToCharArray()[RNG.GetInstance().Next(0, ValidPromoCodeCharacters.Length)];
            //promoCode += ValidPromoCodeWithSpecialCharacters.ToCharArray()[RNG.GetInstance().Next(0, ValidPromoCodeWithSpecialCharacters.Length)];
        }
    }
    public bool IsUsed { get => isUsed; set => isUsed = false; }

    //Make it so it get assigned automaticaly inside the GameManager (when it is created) instead of inside the class
    public double PromoCodeAction
    {
        get { return promoCodeAction; }
        set
        {
            promoCodeAction = (this.PromoCodeType == PromoCodeTypes.MONEY_MULTIPLIER.ToString()) ? RNG.GetInstance().NextDouble() % 3 * 0.5 + 2 : (this.PromoCodeType == PromoCodeTypes.XP_GAIN.ToString()) ? RNG.GetInstance().Next() % 41 * 50 + 100 : RNG.GetInstance().Next() % 41 * 50 + 100;
        }
    }

    public EntityPromoCodes()
    {
        PromoCodeType = promoCodeType;
        PromoCode = promoCode;
        IsUsed = isUsed;
        PromoCodeAction = promoCodeAction;
    }

    //public EntityPromoCodes(string promoCodeType, string promoCode, bool isUsed, double promoCodeAction)
    //{
    //    PromoCodeType = promoCodeType;
    //    PromoCode = promoCode;
    //    IsUsed = isUsed;
    //    PromoCodeAction = promoCodeAction;
    //}

    //Testing Purposes
    public override string ToString()
    {
        return PromoCodeType.ToString() + " | " + PromoCode + " | " + IsUsed.ToString();
    }
}
