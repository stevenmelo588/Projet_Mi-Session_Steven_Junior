
using System;
using UnityEngine;

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
    private int promoCodeAction = 0;

    //public string PromoCode { get => ; set; }
    //public string PromoCode { get => promoCode; set => promoCode = value; }
    public string PromoCodeType { get => promoCodeType; set => promoCodeType = ((PromoCodeTypes)RNG.GetInstance().Next(1, 4)).ToString(); }

    public bool IsUsed { get => isUsed; set => isUsed = false; }

    //Make it so it get assigned automaticaly inside the GameManager (when it is created) instead of inside the class
    public int PromoCodeAction
    {
        get { return promoCodeAction; }
        set
        {
            // if(this.PromoCodeType == PromoCodeTypes.MONEY_MULTIPLIER.ToString())
            //     promoCodeAction = RNG.GetInstance().Next() % 3 * 1 + 1;
            // else
            // {
            //     // promoCodeAction = 100;
            //     // Mathf.Clamp(promoCodeAction, 100, 2000);
            //     // for(int i = 100; i < 2000; i += 50)
            //     promoCodeAction = 50;
            // }
                // for(int i = 0; i < 100; )
            promoCodeAction = (this.PromoCodeType == PromoCodeTypes.MONEY_MULTIPLIER.ToString()) ? RNG.GetInstance().Next(2, 3) * 1 : (this.PromoCodeType == PromoCodeTypes.XP_GAIN.ToString()) ? RNG.GetInstance().Next(1, 41) * 50 : RNG.GetInstance().Next(1, 41) * 50;
        }
    }

    public string PromoCode
    {
        get { return promoCode; }
        set
        {
            for (int j = 0; j < 12; j++)
                promoCode = (this.PromoCodeType == PromoCodeTypes.MONEY_MULTIPLIER.ToString()) ? string.Concat("MONEYx", this.PromoCodeAction.ToString()) : (this.PromoCodeType == PromoCodeTypes.XP_GAIN.ToString()) ? string.Concat("XP", this.PromoCodeAction.ToString()) : string.Concat("MONEY", this.PromoCodeAction.ToString()); 
                // promoCode += ValidPromoCodeCharacters.ToCharArray()[RNG.GetInstance().Next(0, ValidPromoCodeCharacters.Length)];
            //promoCode += ValidPromoCodeWithSpecialCharacters.ToCharArray()[RNG.GetInstance().Next(0, ValidPromoCodeWithSpecialCharacters.Length)];
        }
    }

    public EntityPromoCodes()
    {
        PromoCodeType = promoCodeType;
        IsUsed = isUsed;
        PromoCodeAction += promoCodeAction;
        PromoCode = promoCode;
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
