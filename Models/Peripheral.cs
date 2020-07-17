using System;
namespace GatewayMusalaTest.Models
{
    public class Peripheral
    {
      public int id {get; set;}
      public int uId {get; set;}  
      public string vendor {get; set;}
      public DateTime dateCreated {get; set;}
      public bool isOnline{get;set;}
      public int IdGateway {get; set;}
    }
}