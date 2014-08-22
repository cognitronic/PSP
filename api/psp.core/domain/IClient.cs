using System;
namespace psp.core.domain
{
    public interface IClient
    {
        string address { get; set; }
        bool addressverified { get; set; }
        DateTime birthdate { get; set; }
        bool birthdaycouponsent { get; set; }
        string city { get; set; }
        DateTime dateregistered { get; set; }
        string email { get; set; }
        string firstname { get; set; }
        MongoDB.Bson.ObjectId Id { get; set; }
        bool isactive { get; set; }
        string lastname { get; set; }
        string phone { get; set; }
        bool receivenotifications { get; set; }
        string sid { get; set; }
        string state { get; set; }
        string zip { get; set; }
    }
}
