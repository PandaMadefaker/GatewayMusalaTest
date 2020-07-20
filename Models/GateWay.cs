using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GatewayMusalaTest.Models;
namespace GatewayMusalaTest.Models
{
    public class GateWay
    {
      public int id {get; set;}
      public string serialNumber {get; set;}  
      public string name {get; set;}
      [IPAddressAttribute]
      public string iPv4Address {get; set;}
      //public ICollection<Peripheral> Peripherals{ get; set;}
    }
}